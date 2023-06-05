using Agape316.Data;
using Agape316.Models;
using Agape316.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agape316.Controllers
{
    public class MealDeliveryController : Controller
    {
        private readonly IMealDelivery _mealDeliveryService;
        private readonly IMealSchedule _mealScheduleService;
        private readonly IEmailSender _emailSender;

        public MealDeliveryController(IMealDelivery mealDeliveryService, IMealSchedule mealScheduleService, IEmailSender emailSender)
        {
            _mealDeliveryService = mealDeliveryService;
            _mealScheduleService = mealScheduleService;
            _emailSender = emailSender;
        }       

        [HttpGet]
        public async Task<IActionResult> OnGetCallMealDeliveryViewComponent(int scheduleId, int id)
        {
            return ViewComponent("MealDelivery", new { scheduleId = scheduleId, id = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditMealDelivery(MealDeliveryModel model)
        {  
            await model.SaveMealDelivery(_emailSender, model, _mealDeliveryService, _mealScheduleService);

            return RedirectToAction("Index", "Home");
        }
    }
}
