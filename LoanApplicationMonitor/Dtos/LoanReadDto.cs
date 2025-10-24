using System.ComponentModel.DataAnnotations;

namespace LoanApplicationMonitor.API.Dtos
{
    public class LoanReadDto
    {
        public int LoanId { get; set; }
        public string ApplicantFullName { get; set; } = string.Empty;
        public int LoanAmount { get; set; }
        public int? CreditScore { get; set; }
        public string? LoanType { get; set; } = string.Empty;
        public string? LoanRequestReason { get; set; } = string.Empty;
        public string? AdminComments { get; set; } = string.Empty;
        public DateTime? UpdatedTime { get; set; }
    }
}
