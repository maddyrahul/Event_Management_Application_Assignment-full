using AutoMapper;
using Business_Layer.Services;
using Data_Access_Layer.DTOs;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // POST: api/Event
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDTO eventDto)
        {
            var result = await _eventService.CreateEvent(eventDto);
            if (result)
                return Ok(new { Status = "Success", Message = "Event created successfully!" });
            return StatusCode(500, new { Status = "Error", Message = "Event creation failed!" });
        }

        // GET: api/Event
        [HttpGet]
        [Authorize(Roles = "Organizer,Attendee")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEvents();
            return Ok(events);
        }

        // PUT: api/Event/{eventId}
        [HttpPut("{eventId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateEvent(string eventId, [FromBody] EventDTO eventDto)
        {
            var result = await _eventService.UpdateEvent(eventId, eventDto);
            if (result)
                return Ok(new { Status = "Success", Message = "Event updated successfully!" });
            return StatusCode(500, new { Status = "Error", Message = "Event update failed!" });
        }

        // DELETE: api/Event/{eventId}
        [HttpDelete("{eventId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            var result = await _eventService.DeleteEvent(eventId);
            if (result)
                return Ok(new { Status = "Success", Message = "Event deleted successfully!" });
            return StatusCode(500, new { Status = "Error", Message = "Event deletion failed!" });
        }

        // GET: api/Event/{eventId}
        [HttpGet("{eventId}")]
        [Authorize(Roles = "Organizer,Attendee")]
        public async Task<IActionResult> GetEventById(string eventId)
        {
            var eventDetails = await _eventService.GetEventById(eventId);

            if (eventDetails == null)
                return NotFound(new { Status = "Error", Message = "Event not found!" });

            return Ok(eventDetails);
        }

    }
}
