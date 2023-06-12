using Microsoft.EntityFrameworkCore;
using Agape316.Data;
using Microsoft.Extensions.Hosting;

namespace Agape316.Services
{
    public class EventService : IEvent
    {
        private readonly ApplicationDbContext _context;
        private readonly IEventDish _eventDishService;

        public EventService(ApplicationDbContext context, IEventDish eventDishService)
        {
            _context = context;
            _eventDishService = eventDishService;   
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
            return _context.Event
                .Include(eventDish => eventDish.EventDishes).OrderByDescending(x => x.Created);
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

        public Event GetByEventId(int eventId)
        {
            var agapeEvent = _context.Event.Where(x => x.Id == eventId)
                .FirstOrDefault();

            if (agapeEvent != null && agapeEvent.EventDishes == null)
            {
                agapeEvent.EventDishes = new List<EventDish>();
            }

            return agapeEvent;
        }

        public void UpdateEvent(Event agapeEvent)
        {
            _context.Update(agapeEvent);
            _context.SaveChanges();
        }

        public IEnumerable<Event> GetFilteredEvents(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new List<Event>();
            }
            return GetAll().Where(evt
                    => evt != null && (evt.Title.ToLower().Contains(searchQuery.ToLower())
                    || evt.Description.ToLower().Contains(searchQuery.ToLower())));
        }
    }
}