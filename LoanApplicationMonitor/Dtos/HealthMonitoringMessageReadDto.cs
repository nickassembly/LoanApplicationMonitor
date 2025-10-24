using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.API.Dtos
{
    public class HealthMonitoringMessageReadDto
    {
        public int Id { get; set; }
        public string SystemName { get; set; } = string.Empty;
        public StatusValue StatusValue { get; set; }
        public string? SystemMessage { get; set; } = string.Empty;
        public DateTime? TestCompleted { get; set; }
    }
}
