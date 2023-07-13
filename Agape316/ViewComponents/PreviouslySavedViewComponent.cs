using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Mvc;
namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "PreviouslySaved")]
    public class PreviouslySavedViewComponent : ViewComponent
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;
        private readonly IMealSchedule _mealScheduleService;
        private readonly IMealDelivery _mealDeliveryService;

        public PreviouslySavedViewComponent(IEvent eventService, IEventDish eventDishService,
            IMealSchedule mealScheduleService, IMealDelivery mealDeliveryService)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
            _mealScheduleService = mealScheduleService;
            _mealDeliveryService = mealDeliveryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new PreviouslySavedModel(_eventService, 
                _eventDishService, 
                _mealScheduleService, 
                _mealDeliveryService);

            return View(model);
        }
    }
}
