using Azure.Identity;
using Azure.Storage.Blobs;
using LoanApplicationMonitor.API;
using LoanApplicationMonitor.API.Mappers;
using LoanApplicationMonitor.Core.Interfaces;
using LoanApplicationMonitor.Data;
using LoanApplicationMonitor.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IHealthMonitoringRepository, HealthMonitoringRepository>();

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
