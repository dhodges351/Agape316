using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agape316.Data
{
    public class Event : IEvent
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }
        public virtual IEnumerable<EventItem> EventItems { get; set; }

        public Task Create(Event eventObj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAll()
        {
            throw new NotImplementedException();
        }

        public Event GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEventDescription(int eventId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEventTitle(int eventId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
