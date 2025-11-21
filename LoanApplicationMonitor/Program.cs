using Azure.Identity;
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

var vaultUri = builder.Configuration["AzureKeyVault:VaultUri"];
if (!string.IsNullOrEmpty(vaultUri))
{
    builder.Configuration.AddAzureKeyVault(new Uri(vaultUri), new DefaultAzureCredential());
}
else
{
    Console.WriteLine("Azure Key Vault URI is not configured.");
}

string connString;

if (builder.Environment.IsDevelopment())
{
    connString = builder.Configuration["ConnectionStrings:LocalGuerraTechNowDbConnection"]
        ?? throw new InvalidOperationException("Missing DB connection string.");
}
else
{
    connString = builder.Configuration["ConnectionStrings:CloudGuerraTechNowDbConnection"]
        ?? throw new InvalidOperationException("Missing DB connection string.");
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IHealthMonitoringRepository, HealthMonitoringRepository>();

builder.Services.AddAutoMapper(typeof(LoanMapperProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("WebAppPolicy", policy =>
        policy.WithOrigins
        (
            "https://localhost:7246",
            "https://loanapplicationmonitorwebapp-f2fdf5gnerh6duhp.centralus-01.azurewebsites.net/"
        )
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddDbContext<LoanApplicationDbContext>(options =>
    options.UseSqlServer(connString, sql => sql.EnableRetryOnFailure()));

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
app.UseCors("WebAppPolicy");

app.Run();
