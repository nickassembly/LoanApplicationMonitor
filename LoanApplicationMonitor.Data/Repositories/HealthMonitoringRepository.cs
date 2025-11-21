using LoanApplicationMonitor.Core.Entities;
using LoanApplicationMonitor.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationMonitor.Data.Repositories
{
    public class HealthMonitoringRepository : IHealthMonitoringRepository
    {
        private readonly LoanApplicationDbContext _healthMessageContext;

        public HealthMonitoringRepository(LoanApplicationDbContext healthMessageContext)
        {
          _healthMessageContext = healthMessageContext;
        }

        public async Task<List<HealthMonitoringMessage>> GetAllAsync()
        {
            return await _healthMessageContext.HealthMonitoringMessages.ToListAsync();
        }
    }
}