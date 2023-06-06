using Agape316.Data;
using Agape316.Services;
using Microsoft.AspNetCore.Mvc;
namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "MealDeliveryList")]
    public class MealDeliveryListViewComponent : ViewComponent
    {
        private readonly IMealSchedule _mealScheduleService;
        private readonly IMealDelivery _mealDeliveryService;

        public MealDeliveryListViewComponent(IMealSchedule mealScheduleService, IMealDelivery mealDeliveryService)
        {
            _mealScheduleService = mealScheduleService;
            _mealDeliveryService = mealDeliveryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mealDeliveryList = _mealDeliveryService.GetAll()?.OrderByDescending(x => x.Created).ToList();
            ViewData["RelatedSchedules"] = _mealScheduleService.GetAll().ToList();
            if (ViewData["RelatedSchedules"] == null)
            {
                List<MealSchedule> relatedMealSchedules = new List<MealSchedule>();
                relatedMealSchedules.Add(new MealSchedule { });
                ViewData["RelatedSchedules"] = relatedMealSchedules;
            }
            return View(mealDeliveryList);
        }
    }
}
