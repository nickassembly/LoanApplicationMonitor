using LoanApplicationMonitor.Core.Entities;
using LoanApplicationMonitor.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanApplicationMonitor.Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanApplicationDbContext _context;

        public LoanRepository(LoanApplicationDbContext context)
        {
            _context = context;
        }

        // todo - refactor search function is IQueryable the best approach? can filter checks be simplified?
        public async Task<List<Loan>> SearchLoansAsync(
            int? loanAmount,
            int? creditScore,
            string? loanType,
            string? loanRequestReason,
            string? adminComments)
        {

            IQueryable<Loan> query = _context.Loan.AsQueryable();


            if (loanAmount.HasValue)
                query = query.Where(l => l.LoanAmount == loanAmount.Value);

            if (creditScore.HasValue)
                query = query.Where(l => l.CreditScore == creditScore.Value);

            if (!string.IsNullOrEmpty(loanType))
                query = query.Where(l => l.LoanType.ToLower() == loanType.ToLower());

            if (!string.IsNullOrEmpty(loanRequestReason))
                query = query.Where(l => EF.Functions.Like(l.LoanRequestReason, $"%{loanRequestReason}%"));

            if (!string.IsNullOrEmpty(adminComments))
                query = query.Where(l => EF.Functions.Like(l.AdminComments, $"%{adminComments}%"));

            return await query.ToListAsync();
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loan.ToListAsync();
        }

        public async Task<Loan?> GetAsync(int loanId)
        {
            return await _context.Loan.FindAsync(loanId);
        }

        public async Task AddAsync(Loan record)
        {
            _context.Loan.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Loan record)
        {
            _context.Loan.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            if (loan != null)
            {
                _context.Loan.Remove(loan);
                await _context.SaveChangesAsync();
            }
        }
    }
}