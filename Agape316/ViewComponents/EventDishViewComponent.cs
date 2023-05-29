using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

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
            if (id > 0)
            {
                ViewData["SelectedAgapeEvent"] = _eventService.GetById(id);
            }
            var model = new EventDishModel(_eventService, _eventDishService, null, id);
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

    public class AgapeEventModel
    {       
        public int EventId { get; set; }
        public string Title { get; set; }
    }
}
