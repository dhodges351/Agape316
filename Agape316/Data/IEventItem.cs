using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agape316.Data
{
    public interface IEventItem
    {        
        EventItem GetByEventItemId(int Id);
		IEnumerable<EventItem> GetAll();
        IEnumerable<EventItem> GetFilteredEventItems(string searchQuery);
        IEnumerable<Event> GetFilteredEvents(Event eventObj, string searchQuery);
		IEnumerable<EventItem> GetEventItemsByEventId(int Id);
        Task Add(EventItem eventItem);
		Task Delete(int Id);
		Task EditEventItem(int id, string newContent);
    }
}
