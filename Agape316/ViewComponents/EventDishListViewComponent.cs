using Agape316.Data;
using Microsoft.AspNetCore.Mvc;
namespace Agape316.ViewComponents
{
    [ViewComponent(Name = "EventDishList")]
    public class EventDishListViewComponent : ViewComponent
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;

        public EventDishListViewComponent(IEvent eventService, IEventDish eventDishService)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var eventDishList = _eventDishService.GetAll().OrderByDescending(x => x.Created).ToList();
            ViewData["RelatedEvents"] = _eventService.GetAll().ToList();
            if (eventDishList != null)
            {
                foreach (var item in eventDishList)
                {
                    if (!string.IsNullOrEmpty(item.ImageUrl))
                    {
                        item.ImageUrl = System.Web.HttpUtility.UrlDecode(item.ImageUrl);
                    }
                }
            }
            return View(eventDishList);
        }
    }
}
