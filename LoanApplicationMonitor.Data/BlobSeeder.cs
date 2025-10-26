using Azure.Storage.Blobs;
using System.Text.Json;

namespace LoanApplicationMonitor.Data
{
    public static class BlobSeeder
    {
        public static async Task SeedAsync(BlobContainerClient containerClient)
        {
            await containerClient.CreateIfNotExistsAsync();

            // Check if file already exists
            var blobClient = containerClient.GetBlobClient("health-monitoring-messages.json");
            if (await blobClient.ExistsAsync())
            {
                Console.WriteLine("Blob data already exists. Skipping seed.");
                return;
            }

            var messages = HealthMonitoringMessageFactory.GetSampleMessages();
            var json = JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true });
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            await blobClient.UploadAsync(stream, overwrite: true);

            Console.WriteLine("Seeded health-monitoring-messages.json successfully.");
        }
    }
}
