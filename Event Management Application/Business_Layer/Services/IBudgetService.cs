using Data_Access_Layer.DTOs;

namespace Business_Layer.Services
{
    public interface IBudgetService
    {
        Task<BudgetDTO> GetBudgetForEvent(string eventId);
        Task<bool> UpdateBudget(string eventId, BudgetDTO budgetDto);
        Task<bool> AddBudget(BudgetDTO budgetDto);
    }
}
