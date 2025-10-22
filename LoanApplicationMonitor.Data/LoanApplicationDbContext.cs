using LoanApplicationMonitor.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationMonitor.Data
{
    public class LoanApplicationDbContext : DbContext
    {
        public LoanApplicationDbContext(DbContextOptions<LoanApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Loan> Loan { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
    }
}
