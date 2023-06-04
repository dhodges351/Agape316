using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "MealSchedule")]
    public class MealScheduleViewComponent : ViewComponent
    {        
        private readonly IMealSchedule _mealScheduleService;

        public MealScheduleViewComponent(IMealSchedule mealScheduleService)
        {
            _mealScheduleService = mealScheduleService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = new MealScheduleModel(_mealScheduleService, id);

            var mealSchedules = _mealScheduleService.GetAll().ToList();
            ViewData["MealScheduleObjects"] = mealSchedules;

            ViewData["States"] = new SelectList(Agape316.Helpers.StateArray.states.ToArray(), "Abbreviations", "Abbreviations");
            
            return View(model);
        }
    }
}
