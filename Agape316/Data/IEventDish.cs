using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agape316.Models;

namespace Agape316.Data
{
    public interface IEventDish
    {
        EventDish GetById(int id);
        Event GetByEventId(int id);
        public IEnumerable<EventDish> GetEventDishesByEventId(int eventId);
        IEnumerable<EventDish> GetAll();
        Task Create(EventDish eventDish);
        Task Delete(int Id);
        void UpdateEventDish(EventDish eventDish);
        IEnumerable<EventDish> GetFilteredEventDishes(string searchQuery);
    }
}
