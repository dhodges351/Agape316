using Agape316.Data;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Agape316.Models
{    
    public class HomeIndexModel
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;
        private readonly IMealSchedule _mealScheduleService;
        private readonly IMealDelivery _mealDeliveryService;

        public HomeIndexModel(IEvent eventService,
            IEventDish eventDishService,
            IMealSchedule mealScheduleService,
            IMealDelivery mealDeliveryService,
            string itemType = null,
            int? selectedId = null)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
            _mealScheduleService = mealScheduleService;
            _mealDeliveryService = mealDeliveryService;

            if (itemType == "Event" && selectedId.HasValue)
            {
                AgapeEvent = _eventService.GetByEventId(selectedId.Value);
            }
        }

        public HomeIndexModel()
        {
        }

        public string name { get; private set; }
        public string email { get; private set; }
        public string subject { get; private set; }
        public string message { get; private set; }
        public string? SearchQuery { get; set; }
        public string? UserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name is required")]
        public string Name
        {
            get => name;
            set => name = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get => email;
            set => email = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(100, ErrorMessage = "Subject is required")]
        public string Subject
        {
            get => subject;
            set => subject = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(1000, ErrorMessage = "Message is required")]
        public string Message
        {
            get => message;
            set => message = value;
        }

        public Event AgapeEvent { get; set; }

        public List<EventModel>? Events { get; set; }
        public List<EventDishModel>? EventDishes { get; set; }
        public List<MealScheduleModel>? MealSchedules { get; set; }
        public List<MealDeliveryModel>? MealDeliveries { get; set; }
        public bool EmptySearchResults { get; set; }
        public int EventCount { get; set; } = 0;
        public int EventDishCount { get; set; } = 0;
        public int MealScheduleCount { get; set; } = 0;
        public int MealDeliveryCount { get; set; } = 0;

        public async Task SendContactUsEmail(IEmailSender emailSender, HomeIndexModel model)
        {
            await emailSender.SendEmailAsync(
                    model.Email,
                    model.Subject,
                    model.Message
            );
        }

        public void GetSearchResults(string searchQuery)
        {
            var events = _eventService.GetFilteredEvents(searchQuery).ToList();
            if (events.Any())
            {
                Events = new List<EventModel>();
                foreach (var evt in events)
                {
                    Events.Add(new EventModel { EditLink = evt.EditLink, Title = evt.Title, Description = evt.Description, EventDate = evt.EventDate, StartTime = evt.StartTime, EndTime = evt.EndTime, ContactEmail = evt.ContactEmail });
                }
                EventCount = Events.Count;
            }

            var eventDishes = _eventDishService.GetFilteredEventDishes(searchQuery).ToList();
            if (eventDishes.Any())
            {
                EventDishes = new List<EventDishModel>();
                foreach (var evtDish in eventDishes)
                {
                    EventDishes.Add(new EventDishModel { EditLink = evtDish.EditLink, Title = evtDish.Title, Description = evtDish.Description, Created = evtDish.Created.Value, ContactEmail = evtDish.ContactEmail });
                }
                EventDishCount = EventDishes.Count;
            }

            var mealSchedules = _mealScheduleService.GetFilteredMealSchedules(searchQuery).ToList();
            if (mealSchedules.Any())
            {
                MealSchedules = new List<MealScheduleModel>();
                foreach (var sched in mealSchedules)
                {
                    MealSchedules.Add(new MealScheduleModel { EditLink = sched.EditLink, Title = sched.Title, Description = sched.Description, StartDate = sched.StartDate, EndDate = sched.EndDate, CoordEmail = sched.CoordEmail });
                }
                MealScheduleCount = MealSchedules.Count;
            }

            var mealDeliveries = _mealDeliveryService.GetFilteredMealDeliveries(searchQuery).ToList();
            if (mealDeliveries.Any())
            {
                MealDeliveries = new List<MealDeliveryModel>();
                foreach (var del in mealDeliveries)
                {
                    MealDeliveries.Add(new MealDeliveryModel { EditLink = del.EditLink, DeliveryDate = del.DeliveryDate, DeliveryTime = del.DeliveryTime, Email = del.Email });
                }
                MealDeliveryCount = MealDeliveries.Count;
            }
        }
    }
}
