using LoanApplicationMonitor.API.Dtos;
using LoanApplicationMonitor.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace LoanApplicationMonitor.WebApp.Pages.HealthMonitoringMessages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IndexModel> _logger;
        private readonly string _apiBaseUrl;

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "";
        }

        public List<HealthMonitoringMessageViewModel> HealthMonitoringMessages { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("BackendApi");
            client.BaseAddress = new Uri(_apiBaseUrl);

            string url = "api/HealthMonitoringMessage";

            try
            {
                var apiResponse = await client.GetFromJsonAsync<List<HealthMonitoringMessageReadDto>>(url);

                if (apiResponse != null && apiResponse.Any())
                {
                    HealthMonitoringMessages = apiResponse.Select(dto => new HealthMonitoringMessageViewModel
                    {
                        id = dto.Id,
                        systemName = dto.SystemName,
                        statusValue = dto.StatusValue,
                        systemMessage = dto.SystemMessage,
                        testCompleted = dto.TestCompleted,
                    }).ToList();
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
