using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.WebApp.Models
{
    public class HealthMonitoringMessageViewModel
    {
        public int id { get; set; }
        public string systemName { get; set; } = string.Empty;
        public StatusValue statusValue { get; set; }
        public string? systemMessage { get; set; } = string.Empty;
        public DateTime? testCompleted { get; set; }
    }
}
