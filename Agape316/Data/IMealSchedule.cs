namespace Agape316.Data
{
    public interface IMealSchedule
    {
        MealSchedule GetById(int id);
        IEnumerable<MealSchedule> GetAll();
        Task Create(MealSchedule mealSchedule);
        Task Delete(int Id);
        void UpdateMealSchedule(MealSchedule mealSchedule);
    }
}
