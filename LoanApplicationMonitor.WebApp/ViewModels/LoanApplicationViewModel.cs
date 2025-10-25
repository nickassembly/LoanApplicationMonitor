
namespace LoanApplicationMonitor.WebApp.Models
{
    public class LoanApplicationViewModel
    {
        public int loanId { get; set; }
        public string applicantFullName { get; set; } = string.Empty;
        public int loanAmount { get; set; }
        public int? creditScore { get; set; }
        public string? loanType { get; set; } = string.Empty;
        public string? loanRequestReason { get; set; } = string.Empty;
        public string? adminComments { get; set; } = string.Empty;
        public DateTime? updatedTime { get; set; }
    }
}