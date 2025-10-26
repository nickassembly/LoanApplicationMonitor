using Azure.Storage.Blobs;
using LoanApplicationMonitor.Data;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationMonitor.API
{
    public class StartupInitializationService : IHostedService
    {
        private readonly IServiceProvider _services;

        public StartupInitializationService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();

            // Get services
            var context = scope.ServiceProvider.GetRequiredService<LoanApplicationDbContext>();
            var blobContainer = scope.ServiceProvider.GetRequiredService<BlobContainerClient>();

            // Apply database migrations 
            try
            {
                Console.WriteLine("Applying database migrations...");
                await context.Database.MigrateAsync(cancellationToken);
                Console.WriteLine("Database migrations applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database migration failed: {ex.Message}");
                throw;
            }

            // Seed Loan application data (local MSSQL Db) 
            try
            {
                DbInitializer.Seed(context);
                Console.WriteLine("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database seeding failed: {ex.Message}");
                throw;
            }

            // Seed Health Monitoring Messages (azurite to emulate blob storage) 
            const int maxRetries = 5;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    await BlobSeeder.SeedAsync(blobContainer);
                    Console.WriteLine("Blob seeding completed successfully.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attempt {i + 1}/{maxRetries} - Blob seeding failed: {ex.Message}");

                    if (i < maxRetries - 1)
                        await Task.Delay(2000, cancellationToken);
                    else
                        throw;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
