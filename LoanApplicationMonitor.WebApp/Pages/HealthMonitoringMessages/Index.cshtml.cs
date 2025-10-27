using LoanApplicationMonitor.Core.Entities;
using LoanApplicationMonitor.WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoanApplicationMonitor.WebApp.Pages.HealthMonitoringMessages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IndexModel> _logger;
        private readonly string _apiBaseUrl;

        private const int PageSize = 25;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "";
        }

        public List<HealthMonitoringMessageViewModel> HealthMonitoringMessages { get; set; } = new();

        public async Task OnGetAsync(int pageNumber = 1)
        {
            CurrentPage = pageNumber;

            var client = _httpClientFactory.CreateClient("BackendApi");
            client.BaseAddress = new Uri(_apiBaseUrl);

            string url = "api/HealthMonitoringMessage";

            try
            {
                var apiResponse = await client.GetFromJsonAsync<List<HealthMonitoringMessage>>(url);

                if (apiResponse != null && apiResponse.Any())
                {
                    var totalCount = apiResponse.Count();
                    TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

                    HealthMonitoringMessages = apiResponse
                        .Select(dto => new HealthMonitoringMessageViewModel
                        {
                            id = dto.Id,
                            systemName = dto.SystemName,
                            statusValue = dto.StatusValue,
                            systemMessage = dto.SystemMessage,
                            testCompleted = dto.TestCompleted,
                        })
                      .Skip((CurrentPage - 1) * PageSize)
                      .Take(PageSize)
                      .ToList();
                }
                else
                {
                    _logger.LogInformation("No monitor messages returned from API");
                    HealthMonitoringMessages = new List<HealthMonitoringMessageViewModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching health monitoring messages from API");
                HealthMonitoringMessages = new List<HealthMonitoringMessageViewModel>();
            }
        }
    }
}