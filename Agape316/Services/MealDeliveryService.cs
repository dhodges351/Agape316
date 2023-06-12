using Agape316.Data;

namespace Agape316.Services
{
    public class MealDeliveryService : IMealDelivery
    {
        private readonly ApplicationDbContext _context;

        public MealDeliveryService(ApplicationDbContext context)
        {
            _context = context;
        }        

        public async Task Create(MealDelivery mealDelivery)
        {
            _context.Add(mealDelivery);
            await _context.SaveChangesAsync();
        }

        public MealDelivery GetById(int id)
        {
            var mealDelivery = _context.MealDelivery.Where(x => x.Id == id)
                .FirstOrDefault();

            return mealDelivery;
        }

        public async Task Delete(int id)
        {
            var mealDelivery = GetById(id);
            if (mealDelivery != null)
            {
                _context.Remove(mealDelivery);
                await _context.SaveChangesAsync();
            }
        }    

        public void UpdateMealDelivery(MealDelivery mealDelivery)
        {
            _context.Update(mealDelivery);
            _context.SaveChanges();
        }

        public IEnumerable<MealDelivery> GetAll()
        {
            var mealDeliveries = _context.MealDelivery;            
            return mealDeliveries.OrderByDescending(x => x.Created);
        }

        public IEnumerable<MealDelivery> GetFilteredMealDeliveries(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new List<MealDelivery>();
            }
            return GetAll().Where(del
                    => del != null && (del.ContactFullName.ToLower().Contains(searchQuery.ToLower())
                    || del.DeliveryDate.ToShortDateString().Contains(searchQuery)));
        }
    }
}