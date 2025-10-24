
namespace LoanApplicationMonitor.WebApp.Models
{
    public class LoanApplicationViewModel
    {
        public string ApplicantFullName { get; set; } = string.Empty;
        public int LoanAmount { get; set; }
        public int? CreditScore { get; set; }
        public string? LoanType { get; set; } = string.Empty;
        public string? LoanRequestReason { get; set; } = string.Empty;
        public string? AdminComments { get; set; } = string.Empty;

        // UI Specific Models
        public bool IsSelected { get; set; }
        public bool IsEditing { get; set; }
        public string ValidationMessage { get; set; } = string.Empty;

    }
}
