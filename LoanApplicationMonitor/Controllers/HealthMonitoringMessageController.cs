using AutoMapper;
using LoanApplicationMonitor.API.Dtos;
using LoanApplicationMonitor.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplicationMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthMonitoringMessageController : Controller
    {
        private readonly IHealthMonitoringRepository _healthRepo;
        private readonly IMapper _mapper;

        public HealthMonitoringMessageController(IHealthMonitoringRepository repo, IMapper mapper) 
        { 
            _healthRepo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _healthRepo.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<HealthMonitoringMessageReadDto>>(messages);
            return Ok(dtos);
        }
    }
}
