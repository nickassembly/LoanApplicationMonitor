namespace LoanApplicationMonitor.Core.Entities
{
    public enum StatusValue
    {
        pass,
        fail,
        warning
    }

    public class HealthMonitoringMessage
    {
        public int Id { get; set; }
        public string SystemName { get; set; } = string.Empty;
        public StatusValue StatusValue { get; set; }
        public string? SystemMessage { get; set; } = string.Empty;
        public DateTime? TestCompleted { get; set; }
    }
}
