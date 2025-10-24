using LoanApplicationMonitor.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace LoanApplicationMonitor.WebApp.Pages.HealthMonitoringMessages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<HealthMonitoringMessageReadDto> HealthMonitoringMessages { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("BackendApi");

            var response = await client.GetAsync("api/HealthMonitoringMessage");
            if (response.IsSuccessStatusCode)
            {
                HealthMonitoringMessages = await response.Content.ReadFromJsonAsync<List<HealthMonitoringMessageReadDto>>() ?? new();
            }
        }
    }
}
