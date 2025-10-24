using LoanApplicationMonitor.API.Dtos;
using LoanApplicationMonitor.WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoanApplicationMonitor.WebApp.Pages.Loans
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public List<LoanApplicationViewModel> Loans { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("BackendApi");
            var url = "api/Loan";

            try
            {
                var apiResponse = await client.GetFromJsonAsync<List<LoanReadDto>>(url);

                _logger.LogInformation("Calling API: {FullUrl}", new Uri(client.BaseAddress!, url));

                if (apiResponse != null && apiResponse.Any())
                {
                    Loans = apiResponse.Select(dto => new LoanApplicationViewModel
                    {
                        ApplicantFullName = dto.ApplicantFullName,
                        LoanAmount = dto.LoanAmount,
                        CreditScore = dto.CreditScore,
                        LoanType = dto.LoanType,
                        LoanRequestReason = dto.LoanRequestReason,
                        AdminComments = dto.AdminComments,
                        IsSelected = false,
                        IsEditing = false,
                        ValidationMessage = string.Empty
                    }).ToList();
                }
                else
                {
                    _logger.LogInformation("No loans returned from API");
                    Loans = new List<LoanApplicationViewModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loans from API");
                Loans = new List<LoanApplicationViewModel>();
            }
        }
    }
}
