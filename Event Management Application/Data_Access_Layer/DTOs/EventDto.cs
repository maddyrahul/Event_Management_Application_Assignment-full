

namespace Data_Access_Layer.DTOs
{
    public class EventDTO
    {
        public string EventId { get; set; } = Guid.NewGuid().ToString();
        public string? EventName { get; set; }

        public DateTime Date { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }

        public string? OrganizerId { get; set; }  // This will link the event to the organizer
    }
}
