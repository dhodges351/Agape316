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

            var stateModels = new List<StateModel>
            {
                new StateModel { StateId = 1, StateAbbreviation = "AL" },
                new StateModel { StateId = 2, StateAbbreviation = "AK" },
                new StateModel { StateId = 3, StateAbbreviation = "AZ" },
                new StateModel { StateId = 4, StateAbbreviation = "CA" },
                new StateModel { StateId = 5, StateAbbreviation = "CO" }
            };

            ViewData["States"] = new SelectList(stateModels, "StateId", "StateAbbreviation");
            
            return View(model);
        }
    }

    public class StateModel
    {       
        public int StateId { get; set; }
        public string StateAbbreviation { get; set; }
    }
}
