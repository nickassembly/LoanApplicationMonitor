using LoanApplicationMonitor.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplicationMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthMonitoringMessageController : Controller
    {
        private readonly IHealthMonitoringRepository _repo;
        public HealthMonitoringMessageController(IHealthMonitoringRepository repo) 
        { 
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    }
}
