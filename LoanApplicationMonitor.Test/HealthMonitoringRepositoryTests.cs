using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentAssertions;
using LoanApplicationMonitor.Core.Entities;
using LoanApplicationMonitor.Data.Repositories;
using Moq;
using System.Text.Json;

namespace LoanApplicationMonitor.Test
{
    public class HealthMonitoringRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnDeserializedMessages()
        {
            var expectedData = new List<HealthMonitoringMessage>
            {
                new HealthMonitoringMessage
                {
                    Id = 1,
                    SystemName = "LoanAPI",
                    StatusValue = StatusValue.pass,
                    SystemMessage = "All tests passed",
                    TestCompleted = DateTime.UtcNow
                },
                new HealthMonitoringMessage
                {
                    Id = 2,
                    SystemName = "MonitorAPI",
                    StatusValue = StatusValue.warning,
                    SystemMessage = "CPU usage high",
                    TestCompleted = DateTime.UtcNow
                }
            };

            var json = JsonSerializer.Serialize(expectedData);

            var blobClientMock = new Mock<BlobClient>();
            var content = BinaryData.FromString(json);
            var downloadResult = BlobsModelFactory.BlobDownloadResult(content);

            var response = Response.FromValue(downloadResult, Mock.Of<Response>());

            blobClientMock
                .Setup(b => b.DownloadContentAsync())
                .ReturnsAsync(response);

            var containerClientMock = new Mock<BlobContainerClient>();
            containerClientMock
                .Setup(c => c.GetBlobClient(It.IsAny<string>()))
                .Returns(blobClientMock.Object);

            var repo = new HealthMonitoringRepository(containerClientMock.Object);

            var result = await repo.GetAllAsync();

            result.Should().BeEquivalentTo(expectedData);
        }
    }
}
