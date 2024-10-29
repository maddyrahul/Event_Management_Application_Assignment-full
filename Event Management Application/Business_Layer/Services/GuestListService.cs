
using Data_Access_Layer.DTOs;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;
using Data_Access_Layer.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class GuestListService : IGuestListService
    {
        private readonly IGuestListRepository _guestListRepository;
        private readonly IAuthRepository _authRepository;

        public GuestListService(IGuestListRepository guestListRepository, IAuthRepository authRepository)
        {
            _guestListRepository = guestListRepository;
            _authRepository = authRepository;   
        }

        public async Task<bool> AddGuest(GuestListDto guestListDto)
        {
            try
            {
                // Get the user by attendeeId
                var user = await _authRepository.GetUserById(guestListDto.AttendeeId);

                if (user == null)
                {
                    return false;  // User doesn't exist
                }

                // Check if the UserName and Email match the guestListDto values
                if (user.UserName != guestListDto.UserName || user.Email != guestListDto.Email)
                {
                    return false;  // Mismatch between UserName or Email
                }

                // Create a new GuestList entity from the DTO
                var guestList = new GuestList
                {
                    EventId = guestListDto.EventId,
                    AttendeeId = guestListDto.AttendeeId,
                    UserName = guestListDto.UserName,
                    Email = guestListDto.Email,
                };

                // Add the guest to the repository
                return await _guestListRepository.AddGuest(guestList);
            }
            catch (Exception ex)
            {
                // Log the exception (use a logger here for detailed logging)
                Console.WriteLine($"Error in AddGuest: {ex.Message}");
                throw new Exception("An error occurred while adding the guest.", ex);
            }
        }



        public async Task<IEnumerable<GuestListDto>> GetGuestsByEvent(string eventId)
        {
            var guestList = await _guestListRepository.GetGuestsByEvent(eventId);
            var guestListDtos = new List<GuestListDto>();

            foreach (var guest in guestList)
            {
                guestListDtos.Add(new GuestListDto
                {
                    EventId = guest.EventId,
                    AttendeeId = guest.AttendeeId,
                    UserName= guest.UserName,
                    Email= guest.Email,
                });
            }
            return guestListDtos;
        }

        public async Task<bool> RemoveGuest(string guestListId)
        {
            return await _guestListRepository.RemoveGuest(guestListId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetGuestListForEvent(string eventId)
        {
            return await _guestListRepository.GetGuestList(eventId);
        }

        public async Task<IEnumerable<GuestListDto>> GetGuestsByAttendee(string attendeeId)
        {
            var guests = await _guestListRepository.GetGuestsByAttendee(attendeeId);
            return guests.Select(guest => new GuestListDto
            {
                GuestListId= guest.GuestListId,
                EventId = guest.EventId,
                AttendeeId = guest.AttendeeId,
                UserName = guest.UserName,
                Email = guest.Email
            });
        }

        public async Task<bool> EditGuest(string guestListId, GuestListDto guestListDto)
        {
            var existingGuest = await _guestListRepository.GetGuestById(guestListId);
            if (existingGuest == null)
                return false;

            // Update guest details
            existingGuest.UserName = guestListDto.UserName;
            existingGuest.Email = guestListDto.Email;

            return await _guestListRepository.EditGuest(existingGuest);
        }

        public async Task<IEnumerable<GuestListDto>> GetAllGuestLists()
        {
            var guests = await _guestListRepository.GetAllGuestLists();
            return guests.Select(guest => new GuestListDto
            {
                GuestListId= guest.GuestListId,
                EventId = guest.EventId,
                AttendeeId = guest.AttendeeId,
                UserName = guest.UserName,
                Email = guest.Email
            });
        }

        // Get guest by GuestListId
        public async Task<GuestListDto> GetGuestById(string guestListId)
        {
            var guest = await _guestListRepository.GetGuestById(guestListId);
            if (guest == null)
                return null;

            return new GuestListDto
            {
                EventId = guest.EventId,
                AttendeeId = guest.AttendeeId,
                UserName = guest.UserName,
                Email = guest.Email
            };
        }
    }
}
