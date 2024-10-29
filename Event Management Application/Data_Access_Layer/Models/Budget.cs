using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Budget
    {
        [Key]
        public string BudgetId { get; set; } = Guid.NewGuid().ToString();

        public string? EventId { get; set; }

        [ForeignKey("EventId")]
        public Event? Event { get; set; }

        public decimal Expenses { get; set; }

        public decimal Revenue { get; set; }
    }
}
