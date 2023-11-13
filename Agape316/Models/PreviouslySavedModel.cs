using Agape316.Data;
using Agape316.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agape316.Models
{
    public class PreviouslySavedModel
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;
        private readonly IMealSchedule _mealScheduleService;
        private readonly IMealDelivery _mealDeliveryService;

        public PreviouslySavedModel(IEvent eventService, IEventDish eventDishService,
            IMealSchedule mealScheduleService, IMealDelivery mealDeliveryService)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
            _mealScheduleService = mealScheduleService;
            _mealDeliveryService = mealDeliveryService;

            var events = _eventService.GetAll().ToList();
            if (events != null && events.Count > 0)
            {
                var agapeEventModels = new List<AgapeEventModel>();
                foreach (var agapeEvent in events)
                {
                    if (!string.IsNullOrEmpty(agapeEvent.Title))
                    {
						agapeEventModels.Add(new AgapeEventModel { EventId = agapeEvent.Id, Title = agapeEvent.Title });
					}                    
                }
                Events = new SelectList(agapeEventModels, "EventId", "Title");
            }

            var eventDishes = _eventDishService.GetAll().ToList();
            if (eventDishes != null && eventDishes.Count > 0)
            {
                var eventDishModels = new List<EventDishModel>();
                foreach (var eventDish in eventDishes)
                {
					if (!string.IsNullOrEmpty(eventDish.Title))
					{
						eventDishModels.Add(new EventDishModel { Id = eventDish.Id, Title = eventDish.Title });
					}					
                }
                EventDishes = new SelectList(eventDishModels, "Id", "Title");
            }

            var mealSchedules = _mealScheduleService.GetAll().ToList();
            if (mealSchedules != null && mealSchedules.Count > 0)
            {
                var mealScheduleModels = new List<MealScheduleModel>();
                foreach (var mealSchedule in mealSchedules)
                {
					if (!string.IsNullOrEmpty(mealSchedule.Title))
					{
						mealScheduleModels.Add(new MealScheduleModel { Id = mealSchedule.Id, Title = mealSchedule.Title });
					}					
                }
                MealSchedules = new SelectList(mealScheduleModels, "Id", "Title");
            }

            var mealDeliveries = _mealDeliveryService.GetAll().ToList();
            if (mealDeliveries != null && mealDeliveries.Count > 0)
            {
                var mealDeliveryModels = new List<MealDeliveryModel>();
                foreach (var mealDelivery in mealDeliveries)
                {
                    mealDeliveryModels.Add(new MealDeliveryModel { Id = mealDelivery.Id, Title = mealDelivery.DeliveryDate.ToShortDateString() });
                }
                MealDeliveries = new SelectList(mealDeliveryModels, "Id", "Title");
            }
        }     

        public IEnumerable<SelectListItem> Events { get; set; }
        public IEnumerable<SelectListItem> EventDishes { get; set; }
        public IEnumerable<SelectListItem> MealSchedules { get; set; }
        public IEnumerable<SelectListItem> MealDeliveries { get; set; }
        public int EventId { get; set; }
        public int Id { get; set; }
    }
}
