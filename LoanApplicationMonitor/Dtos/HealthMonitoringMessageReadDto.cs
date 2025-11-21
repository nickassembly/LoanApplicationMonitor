using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.API.Dtos
{
    public class HealthMonitoringMessageReadDto
    {
        public int MessageId { get; set; }
        public string SystemName { get; set; } = string.Empty;
        public string StatusValue { get; set; } = string.Empty;
        public string? SystemMessage { get; set; } = string.Empty;
        public DateTime? TestCompleted { get; set; }
    }
}
