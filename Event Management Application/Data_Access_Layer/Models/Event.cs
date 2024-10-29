

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Models
{
    public class Event
    {
        [Key]
        public string EventId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string? EventName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }

        // Foreign Key to RegisterModel for the Event Organizer
        public string? OrganizerId { get; set; }

        [ForeignKey("OrganizerId")]
        public ApplicationUser? Organizer { get; set; }

        // Many-to-many relationship with Attendees
        public ICollection<ApplicationUser> Attendees { get; set; } = new List<ApplicationUser>();
    }
}
