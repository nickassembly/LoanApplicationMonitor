using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.WebApp.Models
{
    public class HealthMonitoringMessageViewModel
    {
        public string SystemName { get; set; } = string.Empty;
        public StatusValue StatusValue { get; set; }
        public string? SystemMessage { get; set; } = string.Empty;
    }
}
