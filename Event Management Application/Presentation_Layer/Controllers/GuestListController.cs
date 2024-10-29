using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business_Layer.Services;
using Data_Access_Layer.DTOs;


namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestListController : ControllerBase
    {
        private readonly IGuestListService _guestListService;

        public GuestListController(IGuestListService guestListService)
        {
            _guestListService = guestListService;
        }

        [Authorize(Roles = "Organizer, Attendee")]
        [HttpPost]
        public async Task<IActionResult> AddGuest([FromBody] GuestListDto guestListDto)
        {
            try
            {
                var result = await _guestListService.AddGuest(guestListDto);

                if (result)
                {
                    return Ok(new { Message = "Guest added successfully" });
                }

                // Return BadRequest when UserName or Email is invalid
                return BadRequest(new { Message = "Username or Email does not exist or mismatch" });
            }
            catch (Exception ex)
            {
                // Log the exception and return 500 status code
                Console.WriteLine($"Error in AddGuest controller: {ex.Message}");
                return StatusCode(500, new { Message = "An internal error occurred" });
            }
        }

        // GET: api/GuestList/event/{eventId}
        [Authorize(Roles = "Organizer")]
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<IEnumerable<GuestListDto>>> GetGuestsByEvent(string eventId)
        {
            var guests = await _guestListService.GetGuestsByEvent(eventId);
            if (guests == null || !guests.Any())
                return NotFound(new { Status = "Error", Message = "No guests found for the event." });
            return Ok(guests);
        }

        // DELETE: api/GuestList/{guestListId}
        [Authorize(Roles = "Organizer,Attendee")]
        [HttpDelete("{guestListId}")]
        public async Task<IActionResult> RemoveGuest(string guestListId)
        {
            var result = await _guestListService.RemoveGuest(guestListId);
            if (result)
                return NoContent();
            return NotFound(new { Message = "Guest not found" });
        }

        // GET: api/GuestList/guestlist/event/{eventId}
        [HttpGet("guestlist/event/{eventId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetGuestList(string eventId)
        {
            var guestList = await _guestListService.GetGuestListForEvent(eventId);
            if (guestList == null || !guestList.Any())
                return NotFound(new { Status = "Error", Message = "Guest list not found for the event." });
            return Ok(guestList);
        }

        // GET: api/GuestList/attendee/{attendeeId}
        [Authorize(Roles = "Organizer, Attendee")]
        [HttpGet("attendee/{attendeeId}")]
        public async Task<ActionResult<IEnumerable<GuestListDto>>> GetGuestsByAttendee(string attendeeId)
        {
            var guests = await _guestListService.GetGuestsByAttendee(attendeeId);
            if (guests == null || !guests.Any())
                return NotFound(new { Status = "Error", Message = "No guests found for the attendee." });

            return Ok(guests);
        }
        // PUT: api/GuestList/{guestListId}
        [Authorize(Roles = "Organizer, Attendee")]
        [HttpPut("{guestListId}")]
        public async Task<IActionResult> EditGuest(string guestListId, [FromBody] GuestListDto guestListDto)
        {
            var result = await _guestListService.EditGuest(guestListId, guestListDto);
            if (result)
                return Ok(new { Message = "Guest updated successfully" });
            return StatusCode(500, new { Message = "Error updating guest" });
        }

        // GET: api/GuestList/all
        [Authorize(Roles = "Organizer")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<GuestListDto>>> GetAllGuestLists()
        {
            var guestLists = await _guestListService.GetAllGuestLists();
            if (guestLists == null || !guestLists.Any())
                return NotFound(new { Status = "Error", Message = "No guests found." });

            return Ok(guestLists);
        }

        // GET: api/GuestList/{guestListId}
        [Authorize(Roles = "Organizer, Attendee")]
        [HttpGet("{guestListId}")]
        public async Task<IActionResult> GetGuestById(string guestListId)
        {
            var guest = await _guestListService.GetGuestById(guestListId);
            if (guest == null)
                return NotFound(new { Message = "Guest not found" });

            return Ok(guest);
        }
    }
}
