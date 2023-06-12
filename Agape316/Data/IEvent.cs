using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agape316.Models;

namespace Agape316.Data
{
    public interface IEvent
    {
        Event GetById(int id);
        Event GetByEventId(int eventId);
        IEnumerable<Event> GetAll();
        Task Create(Event agapeEvent);
        Task Delete(int id);
        void UpdateEvent(Event agapeEvent);
        IEnumerable<Event> GetFilteredEvents(string searchQuery);
    }
}
