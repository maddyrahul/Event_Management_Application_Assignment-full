using Business_Layer.Services;
using Data_Access_Layer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // GET: api/Budget/{eventId}
        [HttpGet("{eventId}")]
        [Authorize(Roles = "Organizer,Attendee")]
        public async Task<IActionResult> GetBudget(string eventId)
        {
            var budget = await _budgetService.GetBudgetForEvent(eventId);
            if (budget == null)
                return NotFound(new { Status = "Error", Message = "Budget not found for the event." });
            return Ok(budget);
        }

        // PUT: api/Budget/{eventId}
        [HttpPut("{eventId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateBudget(string eventId, [FromBody] BudgetDTO budgetDto)
        {
            var result = await _budgetService.UpdateBudget(eventId, budgetDto);
            if (result)
                return Ok(new { Status = "Success", Message = "Budget updated successfully!" });
            return StatusCode(500, new { Status = "Error", Message = "Budget update failed!" });
        }

        // POST: api/Budget
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> AddBudget([FromBody] BudgetDTO budgetDto)
        {
            var result = await _budgetService.AddBudget(budgetDto);
            if (result)
                return Ok(new { Status = "Success", Message = "Budget added successfully!" });
            return StatusCode(500, new { Status = "Error", Message = "Budget addition failed!" });
        }
    }
}
