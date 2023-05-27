using Agape316.Data;
using Microsoft.AspNetCore.Mvc;
namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "Event")]
    public class EventViewComponent : ViewComponent
    {
        private readonly IEvent _eventService;

        public EventViewComponent(IEvent eventService)
        {
            _eventService = eventService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var agapeEvent = _eventService.GetById(id);
            return View(agapeEvent);
        }
    }
}
