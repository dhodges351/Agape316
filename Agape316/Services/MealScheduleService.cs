using Agape316.Data;

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

        IEnumerable<MealSchedule> IMealSchedule.GetAll()
        {
            var mealSchedules = _context.MealSchedule;

            return mealSchedules.OrderByDescending(x => x.Created);
        }
    }
}