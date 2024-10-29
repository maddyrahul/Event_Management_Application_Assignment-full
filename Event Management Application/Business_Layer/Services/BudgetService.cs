using Data_Access_Layer.DTOs;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;
using Microsoft.Extensions.Logging;

namespace Business_Layer.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<BudgetDTO> GetBudgetForEvent(string eventId)
        {
            var budget = await _budgetRepository.GetBudgetForEvent(eventId);

            if (budget == null)
            {
                // If no budget exists, return a default or empty budget DTO
                return new BudgetDTO
                {
                    EventId = eventId,  // Use eventId from the input
                    Expenses = 0,       // Default value for expenses
                    Revenue = 0         // Default value for revenue
                };
            }

            // If budget exists, map it to BudgetDTO
            return new BudgetDTO
            {
                EventId = budget.EventId,
                Expenses = budget.Expenses,
                Revenue = budget.Revenue
            };
        }


        public async Task<bool> UpdateBudget(string eventId, BudgetDTO budgetDto)
        {
            var existingBudget = await _budgetRepository.GetBudgetForEvent(eventId);

            if (existingBudget != null)
            {
                existingBudget.Expenses = budgetDto.Expenses;
                existingBudget.Revenue = budgetDto.Revenue;
                return await _budgetRepository.UpdateBudget(existingBudget);
            }

            return false;
        }
        public async Task<bool> AddBudget(BudgetDTO budgetDto)
        {
            var newBudget = new Budget
            {
                EventId = budgetDto.EventId,
                Expenses = budgetDto.Expenses,
                Revenue = budgetDto.Revenue
            };

            await _budgetRepository.AddBudget(newBudget);
            return true;
        }

    }
}
