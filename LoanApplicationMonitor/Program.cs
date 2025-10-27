using Azure.Identity;
using Azure.Storage.Blobs;
using LoanApplicationMonitor.API;
using LoanApplicationMonitor.API.Mappers;
using LoanApplicationMonitor.Core.Interfaces;
using LoanApplicationMonitor.Data;
using LoanApplicationMonitor.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// TODO - update config for azure hosting
//if (builder.Environment.IsProduction())
//{
//    var keyVaultUri = builder.Configuration["AzureKeyVault:VaultUri"];
//    if (!string.IsNullOrEmpty(keyVaultUri))
//    {
//        builder.Configuration.AddAzureKeyVault(
//            new Uri(keyVaultUri),
//            new DefaultAzureCredential());
//    }
//}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IHealthMonitoringRepository, HealthMonitoringRepository>();

builder.Services.AddAutoMapper(typeof(LoanMapperProfile).Assembly);

// service to run data seeding in the background for both MSSQL Db and blob storage
builder.Services.AddHostedService<StartupInitializationService>();

// key vault configuration for non-dev environments
var vaultUri = builder.Configuration["AzureKeyVault:VaultUri"];
if (!string.IsNullOrEmpty(vaultUri) && !builder.Environment.IsDevelopment())
{
    builder.Configuration.AddAzureKeyVault(new Uri(vaultUri), new DefaultAzureCredential());
    Console.WriteLine("Loaded secrets from Azure Key Vault");
}
else
{
    Console.WriteLine("Skipping Azure Key Vault (Development or VaultUri not set)");
}

var connString = builder.Configuration.GetConnectionString("LoanApplicationDbConnection")
    ?? throw new InvalidOperationException("Missing DB connection string.");

builder.Services.AddDbContext<LoanApplicationDbContext>(options =>
    options.UseSqlServer(connString, sql => sql.EnableRetryOnFailure()));

// blob storage for health message monitoring data
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var blobConn = config.GetConnectionString("HealthMonitoringDataConnection")
                   ?? throw new InvalidOperationException("Blob Storage connection string missing.");
    var containerName = config["BlobStorage:ContainerName"] ?? "healthmonitoringdata";

    var blobServiceClient = new BlobServiceClient(blobConn);
    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

    // retry logic to mitigate slowness on initial connection
    const int maxRetries = 5;
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            containerClient.CreateIfNotExists();
            Console.WriteLine($"Blob container '{containerName}' ready at {containerClient.Uri}");
            return containerClient;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attempt {i + 1}/{maxRetries} - Could not connect to Blob Storage: {ex.Message}");
            if (i < maxRetries - 1)
                Thread.Sleep(2000);
        }
    }

    throw new InvalidOperationException("Failed to connect to Blob Storage after multiple attempts.");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
