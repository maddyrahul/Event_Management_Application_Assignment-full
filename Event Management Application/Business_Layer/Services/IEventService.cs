using Data_Access_Layer.DTOs;

namespace Business_Layer.Services
{
    public interface IEventService
    {
        Task<bool> CreateEvent(EventDTO eventDto);
        Task<IEnumerable<EventDTO>> GetAllEvents();
        Task<bool> UpdateEvent(string eventId, EventDTO eventDto);
        Task<bool> DeleteEvent(string eventId);
        Task<EventDTO> GetEventById(string eventId);
    }
}
