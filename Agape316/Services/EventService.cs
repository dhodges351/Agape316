using Microsoft.EntityFrameworkCore;
using Agape316.Data;
using Microsoft.Extensions.Hosting;

namespace Agape316.Services
{
    public class EventService : IEvent
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Event agapeEvent)
        {
            _context.Add(agapeEvent);
            await _context.SaveChangesAsync();
        }        

        public async Task Delete(int id)
        {
            var agapeEvent = GetById(id);
            if (agapeEvent != null)
            {
                _context.Remove(agapeEvent);
                await _context.SaveChangesAsync();
            }            
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _context.Event
                        .Include(f => f.EventDishes);

            return events.OrderByDescending(x => x.Created);
        }

        public Event GetById(int id)
        {
            var agapeEvent = _context.Event.Where(x => x.Id == id)
                .Include(f => f.EventDishes)
                .FirstOrDefault();

            if (agapeEvent != null && agapeEvent.EventDishes == null)
            {
                agapeEvent.EventDishes = new List<EventDish>();
            }

            return agapeEvent;
        }        

        public async Task UpdateEvent(Event agapeEvent)
        {
            _context.Update(agapeEvent);
            await _context.SaveChangesAsync();
        }
    }
}