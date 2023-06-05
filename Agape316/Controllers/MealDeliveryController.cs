using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agape316.Controllers
{
    public class MealDeliveryController : Controller
    {
        private readonly IMealDelivery _mealDeliveryService;
        private readonly IEmailSender _emailSender;

        public MealDeliveryController(IMealDelivery mealDeliveryService, IEmailSender emailSender)
        {
            _mealDeliveryService = mealDeliveryService;
            _emailSender = emailSender;
        }       

        [HttpGet]
        public async Task<IActionResult> OnGetCallMealDeliveryViewComponent(int eventId, int id)
        {
            return ViewComponent("MealDelivery", new { eventId = eventId, id = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditMealDelivery(MealDeliveryModel model)
        {  
            await model.SaveMealDelivery(_emailSender, model, _mealDeliveryService);

            return RedirectToAction("Index", "Home");
        }
    }
}
