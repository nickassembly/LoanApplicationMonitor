using Azure.Storage.Blobs;
using LoanApplicationMonitor.Core.Entities;
using LoanApplicationMonitor.Core.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LoanApplicationMonitor.Data.Repositories
{
    public class HealthMonitoringRepository : IHealthMonitoringRepository
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly string _blobFileName = "health-monitoring-messages.json";

        public HealthMonitoringRepository(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
        }

        public async Task<List<HealthMonitoringMessage>> GetAllAsync()
        {
            var blobClient = _blobContainerClient.GetBlobClient(_blobFileName);

            var downloadResult = await blobClient.DownloadContentAsync();
            var json = downloadResult.Value.Content.ToString();

            var data = JsonSerializer.Deserialize<List<HealthMonitoringMessage>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            return data ?? new List<HealthMonitoringMessage>();
        }
    }
}