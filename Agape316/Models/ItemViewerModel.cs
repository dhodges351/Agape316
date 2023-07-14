using Agape316.Data;
using Agape316.Enums;
using Agape316.Helpers;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class ItemViewerModel
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;
        private readonly IMealSchedule _mealScheduleService;
        private readonly IMealDelivery _mealDeliveryService;
        public int SelectedId { get; set; } = 0;
        public Event AgapeEvent { get; set; }
        public EventDish EventDish { get; set; }
        public MealSchedule MealSchedule { get; set; }
        public MealDelivery MealDelivery { get; set; }  

        public ItemViewerModel(IEvent eventService, IEventDish eventDishService,
            IMealSchedule mealScheduleService, IMealDelivery mealDeliveryService)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
            _mealScheduleService = mealScheduleService;
            _mealDeliveryService = mealDeliveryService;
        }        
    }
}
