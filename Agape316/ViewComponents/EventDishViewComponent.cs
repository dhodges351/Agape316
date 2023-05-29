using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "EventDish")]
    public class EventDishViewComponent : ViewComponent
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;

        public EventDishViewComponent(IEvent eventService, IEventDish eventDishService)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = new EventDishModel(_eventService, _eventDishService, null, id);
            var agapeEvents = _eventService.GetAll();
            ViewData["AgapeEvents"] = new SelectList(agapeEvents.ToList(), "Id", "Title");
            return View(model);
        }
    }
}
