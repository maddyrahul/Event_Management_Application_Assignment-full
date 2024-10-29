using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTOs
{
    public class GuestListDto
    {
        public string GuestListId { get; set; } = Guid.NewGuid().ToString();
        public string? EventId { get; set; }
        public string? AttendeeId { get; set; }
        public string? UserName { get; set; }  // Changed to username
        public string? Email { get; set; }     // Assuming you're tracking emails
    }
}
