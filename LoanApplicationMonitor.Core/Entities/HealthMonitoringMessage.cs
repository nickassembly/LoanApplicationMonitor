namespace LoanApplicationMonitor.Core.Entities
{
    public class HealthMonitoringMessage
    {
        public int MessageId { get; set; }
        public string SystemName { get; set; } = string.Empty;
        public string StatusValue { get; set; } = string.Empty;
        public string? SystemMessage { get; set; } = string.Empty;
        public DateTime? TestCompleted { get; set; }
    }
}
