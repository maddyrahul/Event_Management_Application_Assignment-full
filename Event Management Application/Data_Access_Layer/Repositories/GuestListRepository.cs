using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class GuestListRepository : IGuestListRepository
    {
        private readonly ApplicationDbContext _context;

        public GuestListRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddGuest(GuestList guestList)
        {
            _context.GuestLists.Add(guestList);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<GuestList>> GetGuestsByEvent(string eventId)
        {
            return await _context.GuestLists
                .Where(gl => gl.EventId == eventId)
                .ToListAsync();
        }

        public async Task<bool> RemoveGuest(string guestListId)
        {
            var guest = await _context.GuestLists.FindAsync(guestListId);
            if (guest != null)
            {
                _context.GuestLists.Remove(guest);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<IEnumerable<ApplicationUser>> GetGuestList(string eventId)
        {
            return await _context.GuestLists
                                 .Where(g => g.EventId == eventId)
                                 .Include(g => g.Attendee)
                                 .Select(g => g.Attendee)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<GuestList>> GetGuestsByAttendee(string attendeeId)
        {
            return await _context.GuestLists
                .Where(g => g.AttendeeId == attendeeId)
                .ToListAsync();
        }

        public async Task<bool> EditGuest(GuestList guestList)
        {
            _context.GuestLists.Update(guestList);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<GuestList> GetGuestById(string guestListId)
        {
            return await _context.GuestLists
                .FirstOrDefaultAsync(g => g.GuestListId == guestListId);
        }

        public async Task<IEnumerable<GuestList>> GetAllGuestLists()
        {
            return await _context.GuestLists.ToListAsync();
        }

        
    }
}
