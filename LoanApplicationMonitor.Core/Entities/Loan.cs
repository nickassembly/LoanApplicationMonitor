using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoanApplicationMonitor.Core.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ApplicantFullName { get; set; } = string.Empty;
        public int LoanAmount { get; set; }
        public int? CreditScore { get; set; }
        [MaxLength(100)]
        public string? LoanType { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? LoanRequestReason { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? AdminComments { get; set; } = string.Empty;
        public DateTime? UpdatedTime { get; set; }
    }
}
