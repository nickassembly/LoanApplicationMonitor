using LoanApplicationMonitor.API.Dtos;
using LoanApplicationMonitor.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace LoanApplicationMonitor.WebApp.Pages.Loans
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IndexModel> _logger;
        private readonly string _apiBaseUrl;
        public string? ErrorMessage { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "";
        }

        public List<LoanApplicationViewModel> Loans { get; set; } = new();

        public async Task OnGetAsync(string? LoanType, string? LoanRequestReason, int? LoanAmount, string? AdminComments)
        {
            var client = _httpClientFactory.CreateClient("BackendApi");
            client.BaseAddress = new Uri(_apiBaseUrl);

            string url;

            if (!string.IsNullOrEmpty(LoanType) ||
                !string.IsNullOrEmpty(LoanRequestReason) ||
                LoanAmount.HasValue ||
                !string.IsNullOrEmpty(AdminComments))
            {
                var query = new List<string>();
                if (!string.IsNullOrEmpty(LoanType)) query.Add($"LoanType={LoanType}");
                if (!string.IsNullOrEmpty(LoanRequestReason)) query.Add($"LoanRequestReason={LoanRequestReason}");
                if (LoanAmount.HasValue) query.Add($"LoanAmount={LoanAmount}");
                if (!string.IsNullOrEmpty(AdminComments)) query.Add($"AdminComments={AdminComments}");
                var queryString = string.Join("&", query);

                url = $"api/Loan/search?{queryString}";
            }
            else
            {
                url = "api/Loan";
            }

            try
            {
                var apiResponse = await client.GetFromJsonAsync<List<LoanReadDto>>(url);

                if (apiResponse != null && apiResponse.Any())
                {
                    Loans = apiResponse.Select(dto => new LoanApplicationViewModel
                    {
                        loanId = dto.LoanId,
                        applicantFullName = dto.ApplicantFullName,
                        loanAmount = dto.LoanAmount,
                        creditScore = dto.CreditScore,
                        loanType = dto.LoanType,
                        loanRequestReason = dto.LoanRequestReason,
                        adminComments = dto.AdminComments,
                        updatedTime = dto.UpdatedTime,
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

        public async Task<IActionResult> OnPostCreateAsync(LoanCreateDto newLoan)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiBaseUrl);
            var response = await client.PostAsJsonAsync("api/Loan", newLoan);

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Create Failed: {response.StatusCode} Error: {errorText}");

                ErrorMessage = "Failed to create loan. Please check the details and try again.";

                await OnGetAsync(null, null, null, null);
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(LoanUpdateDto updatedLoan)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiBaseUrl);
            var response = await client.PutAsJsonAsync($"api/Loan/{updatedLoan.LoanId}", updatedLoan);

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Update Failed: {response.StatusCode} Error: {errorText}");

                ErrorMessage = "Failed to update loan. Please check the details and try again.";

                await OnGetAsync(null, null, null, null);
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiBaseUrl);
            await client.DeleteAsync($"api/Loan/{id}");

            return RedirectToPage();
        }
    }
}
