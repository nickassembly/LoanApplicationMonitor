using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.Core.Interfaces
{
    public interface ILoanRepository
    {
        Task<List<Loan>> SearchLoansAsync
            (
                int? loanAmount,
                int? creditScore,
                string? loanType,
                string? loanRequestReason,
                string? adminComments
            );
        Task<List<Loan>> GetAllAsync();
        Task<Loan?> GetAsync(int loanId);
        Task AddAsync(Loan record);
        Task UpdateAsync(Loan record);
        Task DeleteAsync(int id);
    }
}
