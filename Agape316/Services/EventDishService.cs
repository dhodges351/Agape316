using Microsoft.EntityFrameworkCore;
using Agape316.Data;
using Microsoft.Extensions.Hosting;

namespace Agape316.Services
{
    public class EventDishService : IEventDish
    {
        private readonly ApplicationDbContext _context;

        public EventDishService(ApplicationDbContext context)
        {
            _context = context;
        }        

        public async Task Create(EventDish eventDish)
        {
            _context.Add(eventDish);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var agapeEventDish = GetById(id);
            if (agapeEventDish != null)
            {
                _context.Remove(agapeEventDish);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<EventDish> GetEventDishesByEventId(int eventId)
        {
            var eventDishes = _context.EventDish.Where(x => x.EventId == eventId);

            return eventDishes.OrderByDescending(x => x.Created);
        }

        public EventDish GetById(int id)
        {
            var agapeEventDish = _context.EventDish.Where(x => x.Id == id)
                .FirstOrDefault();

            return agapeEventDish;
        }

        public Event GetByEventId(int eventId)
        {
            var agapeEvent = _context.Event.Where(x => x.Id == eventId)
                .FirstOrDefault();

            return agapeEvent;
        }

        public void UpdateEventDish(EventDish eventDish)
        {
            _context.Update(eventDish);
            _context.SaveChanges();
        }

        public IEnumerable<EventDish> GetAll()
        {
            var eventDishes = _context.EventDish;

            return eventDishes.OrderByDescending(x => x.Created);
        }

        public IEnumerable<EventDish> GetFilteredEventDishes(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new List<EventDish>();
            }
            return GetAll().Where(evt
                    => evt != null && (evt.Title.ToLower().Contains(searchQuery.ToLower())
                    || evt.Description.ToLower().Contains(searchQuery.ToLower())));
        }
    }
}