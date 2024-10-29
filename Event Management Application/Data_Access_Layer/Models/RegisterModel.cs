using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Data_Access_Layer.Models
{
    public class RegisterModel
    {
        [Key]  // Primary Key
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Role { get; set; }  // Role can be "Organizer" or "Attendee"


        // Navigation property for Organizer's Events
        [JsonIgnore]
        public ICollection<Event> Events { get; set; } = new List<Event>();  // For Organizer

        // Navigation property for Attendee's Registered Events
        [JsonIgnore]
        public ICollection<Event> RegisteredEvents { get; set; } = new List<Event>();  // For Attendee


    }
}
