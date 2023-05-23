using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agape316.Models;

namespace Agape316.Data
{
    public interface IEventDish
    {
        Event GetById(int id);
        Event GetByEventId(int eventId);
        IEnumerable<EventDish> GetAll();
        Task Create(EventDish eventDish);
        Task Delete(int Id);
        Task UpdateEventDish(EventDish eventDish);
    }
}
