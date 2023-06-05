using Agape316.Data;
using Agape316.Models;
using Agape316.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "MealDelivery")]
    public class MealDeliveryViewComponent : ViewComponent
    {        
        private readonly IMealDelivery _mealDeliveryService;
        private readonly IMealSchedule _mealScheduleService;

        public MealDeliveryViewComponent(IMealSchedule mealScheduleService, IMealDelivery mealDeliveryService)
        {
            _mealScheduleService = mealScheduleService;
            _mealDeliveryService = mealDeliveryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int scheduleId, int id)
        {
            if (scheduleId > 0)
            {
                ViewData["SelectedMealSchedule"] = _mealScheduleService.GetById(scheduleId);
            }
            var model = new MealDeliveryModel(_mealScheduleService, _mealDeliveryService, scheduleId, id);

            var mealSchedules = _mealScheduleService.GetAll().ToList();
            var mealScheduleModels = new List<MealScheduleModel>();
            if (mealSchedules != null && mealSchedules.Count > 0)
            {
                foreach (var mealSchedule in mealSchedules)
                {
                    mealScheduleModels.Add(new MealScheduleModel { Id = mealSchedule.Id, Title = mealSchedule.Title });
                }
            }
            else
            {
                mealScheduleModels.Add(new MealScheduleModel { Id = 0, Title = "Please Select" });
            }

            ViewData["MealSchedules"] = new SelectList(mealScheduleModels, "Id", "Title");
            return View(model);
        }
    }
}
