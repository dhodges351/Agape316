using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agape316.Controllers
{
    public class MealScheduleController : Controller
    {
        private readonly IMealSchedule _mealScheduleService;
        private readonly IEmailSender _emailSender;

        public MealScheduleController(IMealSchedule mealScheduleService, IEmailSender emailSender)
        {
            _mealScheduleService = mealScheduleService;
            _emailSender = emailSender;
        }       

        [HttpGet]
        public async Task<IActionResult> OnGetCallMealScheduleViewComponent(int id)
        {
            return ViewComponent("MealSchedule", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditMealSchedule(MealScheduleModel model)
        {  
            await model.SaveMealSchedule(_emailSender, model, _mealScheduleService);

            return RedirectToAction("Index", "Home");
        }
    }
}
