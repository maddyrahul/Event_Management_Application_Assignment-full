using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class GuestList
    {
        [Key]
        public string GuestListId { get; set; } = Guid.NewGuid().ToString();

        public string? EventId { get; set; }
 
        public string? UserName { get; set; }  // Changed to username
        public string? Email { get; set; }     // Assuming you're tracking emails

        [ForeignKey("EventId")]
        public Event? Event { get; set; }

        public string? AttendeeId { get; set; }

        [ForeignKey("AttendeeId")]
        public ApplicationUser? Attendee { get; set; }
    }
}
