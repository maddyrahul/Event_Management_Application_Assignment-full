using Data_Access_Layer.DTOs;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;
namespace Business_Layer.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> CreateEvent(EventDTO eventDto)
        {
            var newEvent = new Event
            {
                EventName = eventDto.EventName,
                Date = eventDto.Date,
                Location = eventDto.Location,
                Description = eventDto.Description,
                OrganizerId = eventDto.OrganizerId
            };

            return await _eventRepository.AddEvent(newEvent);
        }

        public async Task<IEnumerable<EventDTO>> GetAllEvents()
        {
            var events = await _eventRepository.GetAllEvents();
            return events.Select(e => new EventDTO
            {
                EventId = e.EventId,
                EventName = e.EventName,
                Date = e.Date,
                Location = e.Location,
                Description = e.Description,
                OrganizerId = e.OrganizerId
            });
        }

        public async Task<bool> UpdateEvent(string eventId, EventDTO eventDto)
        {
            var existingEvent = await _eventRepository.GetEventById(eventId);

            if (existingEvent != null)
            {
                existingEvent.EventName = eventDto.EventName;
                existingEvent.Date = eventDto.Date;
                existingEvent.Location = eventDto.Location;
                existingEvent.Description = eventDto.Description;

                return await _eventRepository.UpdateEvent(existingEvent);
            }

            return false;
        }
        public async Task<EventDTO> GetEventById(string eventId)
        {
            var eventEntity = await _eventRepository.GetEventById(eventId);

            if (eventEntity == null)
                return null;

            // Manually map the Event entity to EventDTO
            return new EventDTO
            {
                EventId = eventEntity.EventId,
                EventName = eventEntity.EventName,
                Date = eventEntity.Date,
                Location = eventEntity.Location,
                Description = eventEntity.Description,
                OrganizerId = eventEntity.OrganizerId
            };
        }

        public async Task<bool> DeleteEvent(string eventId)
        {
            return await _eventRepository.DeleteEvent(eventId);
        }
    }
}
