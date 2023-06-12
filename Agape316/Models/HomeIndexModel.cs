using Agape316.Data;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;using System.Linq;

namespace Agape316.Models
{    
    public class HomeIndexModel
    {
        private readonly IEvent _eventService;

        public HomeIndexModel(IEvent eventService, string searchQuery)
        {
            _eventService = eventService;
            var events = _eventService.GetFilteredEvents(searchQuery).ToList();
            if (events.Any())
            {
                Events = new List<EventModel>();
                foreach (var evt in events)
                {
                    Events.Add(new EventModel { Title = evt.Title, Description = evt.Description, EventDate = evt.EventDate, StartTime = evt.StartTime, EndTime = evt.EndTime, ContactEmail = evt.ContactEmail });
                }
            }
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

        public List<EventModel>? Events { get; set; }
        public List<EventDishModel>? EventDishes { get; set; }
        public List<MealScheduleModel>? MealSchedules { get; set; }
        public List<MealDeliveryModel>? MealDeliveries { get; set; }
        public bool EmptySearchResults { get; set; }
        public int EventCount { get; set; } = 0;

        public async Task SendContactUsEmail(IEmailSender emailSender, HomeIndexModel model)
        {
            await emailSender.SendEmailAsync(
                    model.Email,
                    model.Subject,
                    model.Message
            );
        }
    }
}
