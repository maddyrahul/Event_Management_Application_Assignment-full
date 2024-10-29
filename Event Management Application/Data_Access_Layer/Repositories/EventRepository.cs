using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
namespace Data_Access_Layer.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddEvent(Event eventModel)
        {
            await _context.Events.AddAsync(eventModel);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _context.Events.Include(e => e.Organizer).ToListAsync();
        }

        public async Task<Event> GetEventById(string eventId)
        {
            return await _context.Events.Include(e => e.Organizer)
                                        .FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<bool> UpdateEvent(Event eventModel)
        {
            _context.Events.Update(eventModel);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEvent(string eventId)
        {
            var eventModel = await GetEventById(eventId);
            if (eventModel != null)
            {
                _context.Events.Remove(eventModel);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
