using Agape316.Data;
using Microsoft.AspNetCore.Mvc;
namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "MealScheduleList")]
    public class MealScheduleListViewComponent : ViewComponent
    {
        private readonly IMealSchedule _mealScheduleService;

        public MealScheduleListViewComponent(IMealSchedule mealScheduleService)
        {
            _mealScheduleService = mealScheduleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mealScheduleList = _mealScheduleService.GetAll()?.OrderByDescending(x => x.Created).ToList();            
            return View(mealScheduleList);
        }
    }
}
