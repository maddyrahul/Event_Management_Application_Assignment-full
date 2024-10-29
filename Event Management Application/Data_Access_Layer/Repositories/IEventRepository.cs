using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public interface IEventRepository
    {
        Task<bool> AddEvent(Event eventModel);
        Task<IEnumerable<Event>> GetAllEvents();
        Task<Event> GetEventById(string eventId);
        Task<bool> UpdateEvent(Event eventModel);
        Task<bool> DeleteEvent(string eventId);
    }
}
