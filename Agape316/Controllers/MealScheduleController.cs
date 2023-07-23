using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Agape316.Controllers
{
    public class MealScheduleController : Controller
    {
        private readonly IMealSchedule _mealScheduleService;
        private readonly IEmailSender _emailSender;
        private readonly INotyfService _toastNotification;

        public MealScheduleController(IMealSchedule mealScheduleService, 
            IEmailSender emailSender,
            INotyfService toastNotification)
        {
            _mealScheduleService = mealScheduleService;
            _emailSender = emailSender;
            _toastNotification = toastNotification;
        }       

        [HttpGet]
        public async Task<IActionResult> OnGetCallMealScheduleViewComponent(int id)
        {
            return ViewComponent("MealSchedule", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditMealSchedule(MealScheduleModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                await model.SaveMealSchedule(_emailSender, model, _mealScheduleService);
                _toastNotification.Success("Thank you, your Meal Schedule has been saved!", 5);
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
