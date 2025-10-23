using LoanApplicationMonitor.API.Dtos;
using LoanApplicationMonitor.Core;
using LoanApplicationMonitor.Core.Entities;
using LoanApplicationMonitor.Core.Interfaces;
using LoanApplicationMonitor.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace LoanApplicationMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : Controller
    {
        // Due to limited scope of business logic, repository is used directly in controller
        // If business logic grows from this point, wrapping the repository in a service layer would likely make sense
        private readonly ILoanRepository _loanRepo;

        public LoanController(ILoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _loanRepo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var loan = await _loanRepo.GetAsync(id);
            return loan != null ? Ok(loan) : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] LoanSearchDto filters)
        {
            var results = await _loanRepo.SearchLoansAsync(
                filters.LoanAmount,
                filters.CreditScore,
                filters.LoanType,
                filters.LoanRequestReason,
                filters.AdminComments);
             return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoanCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var loan = new Loan
            {
                ApplicantFullName = createDto.ApplicantFullName,
                LoanType = createDto.LoanType,
                LoanAmount = createDto.LoanAmount,
                CreditScore = createDto.CreditScore,
                LoanRequestReason = createDto.LoanRequestReason,
                AdminComments = createDto.AdminComments,
                UpdatedTime = DateTime.UtcNow
            };

            try
            {
                await _loanRepo.AddAsync(loan);
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error saving Loan to the database.", ex);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoanUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var loanToUpdate = await _loanRepo.GetAsync(id);

            if (loanToUpdate == null) return NotFound();

            loanToUpdate.ApplicantFullName = updateDto.ApplicantFullName;
            loanToUpdate.LoanType = updateDto.LoanType;
            loanToUpdate.LoanAmount = updateDto.LoanAmount;
            loanToUpdate.CreditScore = updateDto.CreditScore;
            loanToUpdate.AdminComments = updateDto.AdminComments;
            loanToUpdate.LoanRequestReason = updateDto.LoanRequestReason;
            loanToUpdate.UpdatedTime = DateTime.UtcNow;

            try
            {
                await _loanRepo.UpdateAsync(loanToUpdate);
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error saving Loan to the database.", ex);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _loanRepo.DeleteAsync(id);
            return NoContent();
        }
    }

}
