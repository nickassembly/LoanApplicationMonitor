using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.Core.Interfaces
{
    public interface IHealthMonitoringRepository
    {
        Task<List<HealthMonitoringMessage>> GetAllAsync();
    }
}
