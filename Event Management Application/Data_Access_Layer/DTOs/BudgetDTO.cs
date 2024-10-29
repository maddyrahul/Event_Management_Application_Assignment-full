using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTOs
{
    public class BudgetDTO
    {
        public string BudgetId { get; set; } = Guid.NewGuid().ToString();
        public string? EventId { get; set; }
        public decimal Expenses { get; set; }
        public decimal Revenue { get; set; }
    }
}
