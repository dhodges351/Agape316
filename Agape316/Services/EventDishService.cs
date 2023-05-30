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

        public void UpdateEventDish(EventDish eventDish)
        {
            _context.Update(eventDish);
            _context.SaveChangesAsync();
        }

        IEnumerable<EventDish> IEventDish.GetAll()
        {
            var eventDishes = _context.EventDish;

            return eventDishes.OrderByDescending(x => x.Created);
        }
    }
}