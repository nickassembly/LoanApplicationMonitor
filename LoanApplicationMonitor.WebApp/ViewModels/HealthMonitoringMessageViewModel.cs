
namespace LoanApplicationMonitor.WebApp.Models
{
    public class HealthMonitoringMessageViewModel
    {
        public int messageId { get; set; }
        public string systemName { get; set; } = string.Empty;
        public string statusValue { get; set; } = string.Empty;
        public string? systemMessage { get; set; } = string.Empty;
        public DateTime? testCompleted { get; set; }
    }
}
