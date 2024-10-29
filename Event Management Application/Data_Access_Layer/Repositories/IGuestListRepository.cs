using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public interface IGuestListRepository
    {
        Task<IEnumerable<ApplicationUser>> GetGuestList(string eventId);
        Task<bool> AddGuest(GuestList guestList);
        Task<IEnumerable<GuestList>> GetGuestsByEvent(string eventId);
        Task<bool> RemoveGuest(string guestListId);
        Task<GuestList> GetGuestById(string guestListId);
        Task<IEnumerable<GuestList>> GetGuestsByAttendee(string attendeeId);

        Task<IEnumerable<GuestList>> GetAllGuestLists();

        Task<bool> EditGuest(GuestList guestList);


    }
}
