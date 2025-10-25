using System.ComponentModel.DataAnnotations;

namespace LoanApplicationMonitor.API.Dtos
{
    public class LoanCreateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string ApplicantFullName { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Loan type must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Loan type cannot exceed 100 characters.")]
        public string LoanType { get; set; } = string.Empty;

        [Required]
        [Range(100, 1000000, ErrorMessage = "Loan request amount must be between 100 and 1000000.")]
        public int LoanAmount { get; set; }

        [Required]
        [Range(100, 900, ErrorMessage = "Credit score must be between 100 and 900.")]
        public int CreditScore { get; set; }

        [MinLength(5, ErrorMessage = "Full name must be at least 5 characters long.")]
        [MaxLength(1000, ErrorMessage = "Full name cannot exceed 1000 characters.")]
        public string? LoanRequestReason { get; set; }

        [MinLength(5, ErrorMessage = "Full name must be at least 5 characters long.")]
        [MaxLength(1000, ErrorMessage = "Full name cannot exceed 1000 characters.")]
        public string? AdminComments { get; set; }
    }
}
