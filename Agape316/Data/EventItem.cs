using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agape316.Data
{
    public class EventItem
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Event Event { get; set; }

        public Task Add(EventItem eventItem)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task EditEventItem(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public EventItem GetByEventItemId(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventItem> GetEventItemsByEventId(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventItem> GetFilteredEventItems(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetFilteredEvents(Event eventObj, string searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}
