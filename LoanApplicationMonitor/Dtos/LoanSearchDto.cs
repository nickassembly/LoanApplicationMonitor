namespace LoanApplicationMonitor.API.Dtos
{
    public class LoanSearchDto
    {
        public int? LoanAmount { get; set; }
        public int? CreditScore { get; set; }
        public string? LoanType { get; set; }
        public string? LoanRequestReason { get; set; }
        public string? AdminComments { get; set; }
    }
}
