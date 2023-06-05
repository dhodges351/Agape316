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
        private readonly IEvent _eventService;

        public MealDeliveryViewComponent(IEvent eventService, IMealDelivery mealDeliveryService)
        {
            _eventService = eventService;
            _mealDeliveryService = mealDeliveryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int eventId, int id)
        {
            if (eventId > 0)
            {
                ViewData["SelectedAgapeEvent"] = _eventService.GetById(eventId);
            }
            var model = new MealDeliveryModel(_eventService, _mealDeliveryService, eventId, id);

            var agapeEvents = _eventService.GetAll().ToList();
            ViewData["AgapeEventObjects"] = agapeEvents;

            var agapeEventModels = new List<AgapeEventModel>();
            if (agapeEvents != null && agapeEvents.Count > 0)
            {
                foreach (var agapeEvent in agapeEvents)
                {
                    agapeEventModels.Add(new AgapeEventModel { EventId = agapeEvent.Id, Title = agapeEvent.Title });
                }
            }
            ViewData["AgapeEvents"] = new SelectList(agapeEventModels, "EventId", "Title");
            return View(model);
        }
    }
}
