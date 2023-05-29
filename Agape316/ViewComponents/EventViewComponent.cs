using Agape316.Data;
using Agape316.Models;
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
            var model = new EventModel(_eventService, id);
            var agapeEvent = _eventService.GetById(id);
            if (agapeEvent != null)
            {
                model.Title = agapeEvent.Title;
                model.Description = agapeEvent.Description;
                model.EventDate = agapeEvent.EventDate;
                model.ImageUrl = agapeEvent.ImageUrl;
                model.Location = agapeEvent.Location;
                model.CategoryId = agapeEvent.CategoryId;
                model.ContactEmail = agapeEvent.ContactEmail;
                model.StartTime = agapeEvent.StartTime;
                model.EndTime = agapeEvent.EndTime;
                model.Notes = agapeEvent.Notes;
                model.SandwichSlots = agapeEvent.SandwichSlots;
                model.SideDishSlots = agapeEvent.SideDishSlots;
                model.MainDishSlots = agapeEvent.MainDishSlots;
                model.DessertSlots = agapeEvent.DessertSlots;
                model.SetUpSlots = agapeEvent.SetUpSlots;
                model.ServeSlots = agapeEvent.ServeSlots;
                model.CleanUpSlots = agapeEvent.CleanUpSlots;
            }
            return View(model);
        }
    }
}
