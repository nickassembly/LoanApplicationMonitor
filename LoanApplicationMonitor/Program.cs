using Azure.Identity;
using Azure.Storage.Blobs;
using LoanApplicationMonitor.API;
using LoanApplicationMonitor.API.Mappers;
using LoanApplicationMonitor.Core.Interfaces;
using LoanApplicationMonitor.Data;
using LoanApplicationMonitor.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register repos
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IHealthMonitoringRepository, HealthMonitoringRepository>();

// configure KV for Azure resources
var vaultUri = builder.Configuration["AzureKeyVault:VaultUri"];
if (!string.IsNullOrEmpty(vaultUri))
{
    builder.Configuration.AddAzureKeyVault(new Uri(vaultUri), new DefaultAzureCredential());
}
else
{
    Console.WriteLine("Azure Key Vault URI is not configured.");
}

var connString = builder.Configuration["ConnectionStrings:LoanApplicationDbConnection"];
builder.Services.AddDbContext<LoanApplicationDbContext>(options =>
        options.UseSqlServer(connString));

// configure Blob Storage client (JSON file to handle Health monitoring messages)
builder.Services.AddSingleton(sp =>
{
    var storageBlobConnection = builder.Configuration["ConnectionStrings:HealthMonitoringDataConnection"];
    var service = new BlobServiceClient(storageBlobConnection);
    return service.GetBlobContainerClient("healthmonitoringdata");
});

builder.Services.AddAutoMapper(typeof(LoanMapperProfile).Assembly);

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
