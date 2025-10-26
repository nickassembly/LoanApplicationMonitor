using LoanApplicationMonitor.Core.Entities; // adjust namespace
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationMonitor.Data
{
    public class LoanApplicationDbContext : DbContext
    {
        public LoanApplicationDbContext(DbContextOptions<LoanApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("Loans");

                entity.HasKey(e => e.LoanId);

                entity.Property(e => e.ApplicantFullName)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.LoanAmount)
                      .IsRequired();

                entity.Property(e => e.CreditScore);

                entity.Property(e => e.LoanType)
                      .HasMaxLength(100);

                entity.Property(e => e.LoanRequestReason)
                      .HasMaxLength(1000);

                entity.Property(e => e.AdminComments)
                     .HasMaxLength(1000);

                entity.Property(e => e.UpdatedTime);
            });
        }
    }
}
