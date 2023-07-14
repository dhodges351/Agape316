using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Mvc;
namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "ItemViewer")]
    public class ItemViewerViewComponent : ViewComponent
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;
        private readonly IMealSchedule _mealScheduleService;
        private readonly IMealDelivery _mealDeliveryService;

        public ItemViewerViewComponent(IEvent eventService, IEventDish eventDishService,
            IMealSchedule mealScheduleService, IMealDelivery mealDeliveryService)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
            _mealScheduleService = mealScheduleService;
            _mealDeliveryService = mealDeliveryService;
        }

        public async Task<Object> InvokeAsync(string itemType,  int selectedId)
        {
            var model = new ItemViewerModel(_eventService, 
                _eventDishService, 
                _mealScheduleService, 
                _mealDeliveryService);

            if (selectedId > 0)
            {
                model.SelectedId = selectedId;
                switch (itemType)
                {
                    case "Event":
                        model.AgapeEvent = _eventService.GetByEventId(selectedId);
                        break;
                    default:
                        break;
                }
            }

            return View(model);
        }
    }
}
