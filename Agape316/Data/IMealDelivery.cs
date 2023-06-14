namespace Agape316.Data
{
    public interface IMealDelivery
    {
        MealDelivery GetById(int id);
        IEnumerable<MealDelivery> GetAll();
        Task Create(MealDelivery mealDelivery);
        Task Delete(int Id);
        void UpdateMealDelivery(MealDelivery mealDelivery);
        IEnumerable<MealDelivery> GetFilteredMealDeliveries(string searchQuery);
    }
}
