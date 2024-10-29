using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public interface IBudgetRepository
    {
        Task<Budget> GetBudgetForEvent(string eventId);
        Task<bool> UpdateBudget(Budget budget);

        Task AddBudget(Budget budget);
    }
}
