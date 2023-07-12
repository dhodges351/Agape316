using Agape316.Data;
using Agape316.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agape316.Models
{
    public class ViewEventsModel
    {
        private readonly IEvent _eventService;

        public ViewEventsModel(IEvent eventService)
        {
            _eventService = eventService;
            var events = _eventService.GetAll().ToList();
            if (events != null && events.Count > 0)
            {
                var agapeEventModels = new List<AgapeEventModel>();
                foreach (var agapeEvent in events)
                {
                    agapeEventModels.Add(new AgapeEventModel { EventId = agapeEvent.Id, Title = agapeEvent.Title });
                }
                Events = new SelectList(agapeEventModels, "EventId", "Title");
            }            
        }     

        public IEnumerable<SelectListItem> Events { get; set; }
        public int EventId { get; set; }

        public Event Event { get; set; }
    }
}
