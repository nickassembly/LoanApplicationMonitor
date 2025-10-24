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
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "";
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
                        loanId = dto.LoanId,
                        applicantFullName = dto.ApplicantFullName,
                        loanAmount = dto.LoanAmount,
                        creditScore = dto.CreditScore,
                        loanType = dto.LoanType,
                        loanRequestReason = dto.LoanRequestReason,
                        adminComments = dto.AdminComments,
                        isSelected = false,
                        isEditing = false,
                        validationMessage = string.Empty
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

        //public async Task<IActionResult> OnPostCreateAsync(LoanCreateDto newLoan)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_apiBaseUrl);

        //    await client.PostAsJsonAsync("api/Loan", newLoan);
        //    return RedirectToPage();
        //}

        //public async Task<IActionResult> OnPostUpdateAsync(LoanUpdateDto updatedLoan)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_apiBaseUrl);

        //    await client.PutAsJsonAsync($"api/Loan/{updatedLoan.LoanId}", updatedLoan);
        //    return RedirectToPage();
        //}

        public async Task<IActionResult> OnPostCreateAsync(LoanCreateDto newLoan)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiBaseUrl);
            var response = await client.PostAsJsonAsync("api/Loan", newLoan);
            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Create failed: {response.StatusCode}");
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(LoanUpdateDto updatedLoan)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiBaseUrl);
            var response = await client.PutAsJsonAsync($"api/Loan/{updatedLoan.LoanId}", updatedLoan);
            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Update failed: {response.StatusCode}");
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
