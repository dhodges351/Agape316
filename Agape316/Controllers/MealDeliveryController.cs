using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Agape316.Controllers
{
    public class MealDeliveryController : Controller
    {
        private readonly IMealDelivery _mealDeliveryService;
        private readonly IMealSchedule _mealScheduleService;
        private readonly IEmailSender _emailSender;
        private readonly INotyfService _toastNotification;

        public MealDeliveryController(IMealDelivery mealDeliveryService, 
            IMealSchedule mealScheduleService, IEmailSender emailSender,
            INotyfService toastNotification)
        {
            _mealDeliveryService = mealDeliveryService;
            _mealScheduleService = mealScheduleService;
            _emailSender = emailSender;
            _toastNotification = toastNotification;
        }       

        [HttpGet]
        public async Task<IActionResult> OnGetCallMealDeliveryViewComponent(int scheduleId, int id)
        {
            return ViewComponent("MealDelivery", new { scheduleId = scheduleId, id = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditMealDelivery(MealDeliveryModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                await model.SaveMealDelivery(_emailSender, model, _mealDeliveryService, _mealScheduleService);
                _toastNotification.Success("Thank you, your Meal Delivery has been saved!", 5);
            }
                
            return RedirectToAction("Index", "Home");
        }
    }
}
