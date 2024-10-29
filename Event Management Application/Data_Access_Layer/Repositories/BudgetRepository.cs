using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;


namespace Data_Access_Layer.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddBudget(Budget budget)
        {
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
        }
        public async Task<Budget> GetBudgetForEvent(string eventId)
        {
            return await _context.Budgets
                                 .FirstOrDefaultAsync(b => b.EventId == eventId);
        }

        public async Task<bool> UpdateBudget(Budget budget)
        {
            _context.Budgets.Update(budget);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
