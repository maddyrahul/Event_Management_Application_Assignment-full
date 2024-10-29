using Data_Access_Layer.Models;
using Data_Access_Layer.DTOs;

namespace Business_Layer.Services
{
    public interface IGuestListService
    {
        Task<IEnumerable<ApplicationUser>> GetGuestListForEvent(string eventId);
        Task<bool> AddGuest(GuestListDto guestListDto);
        Task<IEnumerable<GuestListDto>> GetGuestsByEvent(string eventId);
        Task<bool> RemoveGuest(string guestListId);
        Task<bool> EditGuest(string guestListId, GuestListDto guestListDto);
        Task<IEnumerable<GuestListDto>> GetGuestsByAttendee(string attendeeId);

        Task<IEnumerable<GuestListDto>> GetAllGuestLists();

        Task<GuestListDto> GetGuestById(string guestListId);

    }
}
