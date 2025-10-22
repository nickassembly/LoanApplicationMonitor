using LoanApplicationMonitor.API.Dtos;
using LoanApplicationMonitor.Core.Entities;
using LoanApplicationMonitor.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplicationMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : Controller
    {
        private readonly ILoanRepository _repo;

        public LoanController(ILoanRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var loan = await _repo.GetAsync(id);
            return loan != null ? Ok(loan) : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] LoanSearchDto filters)
        {
            var results = await _repo.SearchLoansAsync(
                filters.LoanAmount,
                filters.CreditScore,
                filters.LoanType,
                filters.LoanRequestReason,
                filters.AdminComments);
             return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Loan loan)
        {
            await _repo.AddAsync(loan);
            return CreatedAtAction(nameof(Get), new { id = loan.LoanId }, loan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Loan loan)
        {
            if (id != loan.LoanId) return BadRequest();
            await _repo.UpdateAsync(loan);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }

}
