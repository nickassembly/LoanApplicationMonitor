using FluentAssertions;
using LoanApplicationMonitor.Core.Entities;
using LoanApplicationMonitor.Data;
using LoanApplicationMonitor.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationMonitor.Test
{
    public class LoanRepositoryTests
    {
        private readonly LoanApplicationDbContext _context;
        private readonly LoanRepository _repo;

        public LoanRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<LoanApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LoanTestDb")
                .Options;

            _context = new LoanApplicationDbContext(options);
            _repo = new LoanRepository(_context);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnLoan_WhenExists()
        {
            var loan = new Loan { ApplicantFullName = "Jane Doe" };
            await _repo.AddAsync(loan);

            var fetchedLoan = await _repo.GetAsync(loan.LoanId);
            fetchedLoan.Should().NotBeNull();
            fetchedLoan.ApplicantFullName.Should().Be("Jane Doe");
        }

        [Fact]
        public async Task AddAsync_ShouldAddLoan()
        {
            var loan = new Loan { ApplicantFullName = "Test User", LoanAmount = 1000 };
            await _repo.AddAsync(loan);

            var allLoans = await _repo.GetAllAsync();
            allLoans.Should().ContainSingle(l => l.ApplicantFullName == "Test User");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingLoan()
        {
            var loan = new Loan { ApplicantFullName = "Jane Doe" };
            await _repo.AddAsync(loan);

            var originalLoan = await _repo.GetAsync(loan.LoanId);
            originalLoan.Should().NotBeNull();

            originalLoan.ApplicantFullName = "Jane Smith";

            await _repo.UpdateAsync(originalLoan);
            var updatedLoan = await _repo.GetAsync(loan.LoanId);

            updatedLoan?.ApplicantFullName.Should().Be("Jane Smith");
        }
    }

}