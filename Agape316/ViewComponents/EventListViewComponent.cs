using Agape316.Data;
using Microsoft.AspNetCore.Mvc;
namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "EventList")]
    public class EventListViewComponent : ViewComponent
    {
        private readonly IEvent _eventService;

        public EventListViewComponent(IEvent eventService)
        {
            _eventService = eventService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var eventList = _eventService.GetAll();
            return View(eventList);
        }
    }
}
