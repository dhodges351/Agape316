using Agape316.Data;
using System.Runtime.CompilerServices;

namespace Agape316.Services
{
    public class MealScheduleService : IMealSchedule
    {
        private readonly ApplicationDbContext _context;

        public MealScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }        

        public async Task Create(MealSchedule mealSchedule)
        {
            _context.Add(mealSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var mealSchedule = GetById(id);
            if (mealSchedule != null)
            {
                _context.Remove(mealSchedule);
                await _context.SaveChangesAsync();
            }
        }    

        public MealSchedule GetById(int id)
        {
            var mealSchedule = _context.MealSchedule.Where(x => x.Id == id)
                .FirstOrDefault();

            return mealSchedule;
        }

        public void UpdateMealSchedule(MealSchedule mealSchedule)
        {
            _context.Update(mealSchedule);
            _context.SaveChanges();
        }

        public IEnumerable<MealSchedule> GetAll()
        {
            var mealSchedules = _context.MealSchedule;

            return mealSchedules.OrderByDescending(x => x.Created);
        }

        public IEnumerable<MealSchedule> GetFilteredMealSchedules(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new List<MealSchedule>();
            }
            return GetAll().Where(sched
                    => sched != null && (sched.CoordEmail.ToLower().Contains(searchQuery.ToLower())
                    || sched.StartDate.ToShortDateString().Contains(searchQuery)
                    || sched.EndDate.ToShortDateString().Contains(searchQuery)
                    || sched.Title.ToLower().Contains(searchQuery.ToLower())
                    || sched.Description.ToLower().Contains(searchQuery.ToLower())));
        }
    }
}