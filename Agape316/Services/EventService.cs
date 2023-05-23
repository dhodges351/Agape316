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

        public async Task Delete(int eventId)
        {
            var agapeEvent = GetById(eventId);
            if (agapeEvent != null)
            {
                _context.Remove(agapeEvent);
                await _context.SaveChangesAsync();
            }            
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events
                .Include(agapeEvent => agapeEvent.EventDishes);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Event GetById(int id)
        {
            var agapeEvent = _context.Events.Where(x => x.Id == id)
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