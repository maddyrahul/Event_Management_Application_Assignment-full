using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Data_Access_Layer.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation property for Organizer's Events
        [JsonIgnore]
        public ICollection<Event> Events { get; set; } = new List<Event>();  // For Organizer

        // Navigation property for Attendee's Registered Events
        [JsonIgnore]
        public ICollection<Event> RegisteredEvents { get; set; } = new List<Event>();  // For Attendee
    }
}
