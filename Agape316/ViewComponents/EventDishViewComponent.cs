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

        public async Task<IViewComponentResult> InvokeAsync(int eventId, int id)
        {
            if (eventId > 0)
            {
                ViewData["SelectedAgapeEvent"] = _eventService.GetById(eventId);
            }

            var model = new EventDishModel(_eventService, _eventDishService, eventId, id);

            var agapeEvents = _eventService.GetAll().ToList();
            var filteredAgapeEvents = new List<Event>();
            
            // Filter out expired events.
            if (agapeEvents != null && agapeEvents.Count > 0)
            {  
                foreach (var agapeEvent in agapeEvents)
                {
                    DateTime date1 = DateTime.Now;                    
                    DateTime date2 = DateTime.Parse($"{agapeEvent.EventDate.ToShortDateString()} {agapeEvent.EndTime}");
                    int result = DateTime.Compare(date1, date2);
                    if (result <= 0)
                    {
                        filteredAgapeEvents.Add(agapeEvent);
                    }                    
                }
            }
            ViewData["AgapeEventObjects"] = filteredAgapeEvents.Count > 0 ? filteredAgapeEvents : agapeEvents;

            var agapeEventModels = new List<AgapeEventModel>();
            if (filteredAgapeEvents != null && filteredAgapeEvents.Count > 0)
            {
                foreach (var agapeEvent in filteredAgapeEvents)
                {
                    agapeEventModels.Add(new AgapeEventModel { EventId = agapeEvent.Id, Title = agapeEvent.Title });
                }

                ViewData["AgapeEvents"] = new SelectList(agapeEventModels, "EventId", "Title");
                return View(model);
            }
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
