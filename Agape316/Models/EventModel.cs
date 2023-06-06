using Agape316.Data;
using Agape316.Enums;
using Agape316.Helpers;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class EventModel
    {
        private readonly IEmailSender _emailSender;
        private readonly IEvent _eventService;
        public string? notes { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public string location { get; private set; }
        public string contactEmail { get; private set; }
        public string startTime { get; private set; }
        public string endTime { get; private set; }
        public string imageUrl { get; private set; }
        public string category { get; private set; }

        public EventModel()
        {
        }

        public EventModel(IEmailSender emailSender, IEvent eventService, int? id = null)
        {
            _emailSender = emailSender;
            _eventService = eventService;
            if (id.HasValue)
            {
                var agapeEvent = _eventService.GetById(id.Value);
                if (agapeEvent != null)
                {
                    Title = agapeEvent.Title;
                    Description = agapeEvent.Description;
                    EventDate = agapeEvent.EventDate;
                    ImageUrl = agapeEvent.ImageUrl;
                    Location = agapeEvent.Location;
                    CategoryId = agapeEvent.CategoryId;
                    ContactEmail = agapeEvent.ContactEmail;
                    StartTime = agapeEvent.StartTime;
                    EndTime = agapeEvent.EndTime;
                    Notes = agapeEvent.Notes;
                    SandwichSlots = agapeEvent.SandwichSlots;
                    SideDishSlots = agapeEvent.SideDishSlots;
                    MainDishSlots = agapeEvent.MainDishSlots;
                    DessertSlots = agapeEvent.DessertSlots;
                    SetUpSlots = agapeEvent.SetUpSlots;
                    ServeSlots = agapeEvent.ServeSlots;
                    CleanUpSlots = agapeEvent.CleanUpSlots;
                }
            }
            _emailSender = emailSender;
        }

        [Required]
        [StringLength(100, ErrorMessage = "Title is required")]
        public string Title
        {
            get => title;
            set => title = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(200, ErrorMessage = "Description is required")]
        public string Description
        {
            get => description;
            set => description = new HtmlSanitizer().Sanitize(value);
        }        

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string? Notes
        {
            get => notes;
            set => notes = new HtmlSanitizer().Sanitize(value == null ? "" : value);
        }

        [Required]
        [StringLength(100, ErrorMessage = "Location is required")]
        public string Location
        {
            get => location;
            set => location = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Contact Email")]
        [StringLength(100, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string ContactEmail
        {
            get => contactEmail;
            set => contactEmail = new HtmlSanitizer().Sanitize(value);
        }

        public DateTime Created { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EventDate { get; set; } = DateTime.Now.Date;
            
        public string ImageUrl
        {
            get => imageUrl;
            set => imageUrl = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:  hh:mm:ss aa}")]        
        public string StartTime
        {
            get => startTime;
            set => startTime = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:  hh:mm:ss aa}")]
        public string EndTime
        {
            get => endTime;
            set => endTime = new HtmlSanitizer().Sanitize(value);
        }
       
        public int? CategoryId { get; set; }
        public int? Id { get; set; }
        public int? SandwichSlots { get; set; }
        public int? SideDishSlots { get; set; }
        public int? MainDishSlots { get; set; }
        public int? DessertSlots { get; set; }
        public int? SetUpSlots { get; set; }
        public int? ServeSlots { get; set; }
        public int? CleanUpSlots { get; set; }

        [Required]        
        [StringLength(50, ErrorMessage = "Category is required")]
        public string Category
        {
            get => category;
            set => category = new HtmlSanitizer().Sanitize(value);
        }

        public IEnumerable<Event> AgapeEvents { get; set; }

        public int GetCategoryId(string category)
        {
            int catId = 0;
            var categories = Enum.GetNames(typeof(CategoryEnum)).ToList();
            if (categories.Contains(category))
            {
                catId = categories.FindIndex(a => a.Contains(category));
            }

            return catId + 1;
        }

        public string GetCategoryName(int categoryId)
        {
            var categoryName = string.Empty;
            var categories = Enum.GetNames(typeof(CategoryEnum)).ToList();
            for (int i = 0; i < categories.Count; i++) 
            {
                if ((i + 1) == categoryId)
                {
                    categoryName = categories[i].ToString();
                    break;
                }
            }
            return categoryName;
        }

        public async Task SaveEvent(IEmailSender emailSender, EventModel model, string filePath, IEvent _eventService)
        {
            if (!model.Id.HasValue)
            {
                var agapeEvent = new Event
                {                    
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    EventDate = model.EventDate,
                    ImageUrl = filePath,
                    Location = model.Location,
                    CategoryId = model.GetCategoryId(model.Category),
                    ContactEmail = model.ContactEmail,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Notes = model.Notes,
                    SandwichSlots = model.SandwichSlots,
                    SideDishSlots = model.SideDishSlots,
                    MainDishSlots = model.MainDishSlots,
                    DessertSlots = model.DessertSlots,
                    SetUpSlots = model.SetUpSlots,
                    ServeSlots = model.ServeSlots,
                    CleanUpSlots = model.CleanUpSlots,
                };
                await _eventService.Create(agapeEvent);
            }
            else
            {
                var agapeEvent = _eventService.GetById(model.Id.Value);
                agapeEvent.Title = model.Title;
                agapeEvent.Description = model.Description;
                agapeEvent.Created = DateTime.Now;
                agapeEvent.EventDate = model.EventDate;
                agapeEvent.ImageUrl = model.ImageUrl;
                agapeEvent.Location = model.Location;
                agapeEvent.CategoryId = model.GetCategoryId(model.Category);
                agapeEvent.ContactEmail = model.ContactEmail;
                agapeEvent.StartTime = model.StartTime;
                agapeEvent.EndTime = model.EndTime;
                agapeEvent.Notes = model.Notes;
                agapeEvent.SandwichSlots = model.SandwichSlots;
                agapeEvent.SideDishSlots = model.SideDishSlots;
                agapeEvent.MainDishSlots = model.MainDishSlots;
                agapeEvent.DessertSlots = model.DessertSlots;
                agapeEvent.SetUpSlots = model.SetUpSlots;
                agapeEvent.ServeSlots = model.ServeSlots;
                agapeEvent.CleanUpSlots = model.CleanUpSlots;

                _eventService.UpdateEvent(agapeEvent);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }                  

            await emailSender.SendEmailAsync(
                    model.ContactEmail,
                    model.Title,
                    $"<h1>Thank you for submitting your Event!</h1>" +
                    $"Title: {model.Title} " +
                    $" <br /> Description: {model.Description} " +
                    $" <br /> Location: {model.Location} " +
                    $" <br /> Event Date: {model.EventDate.ToShortDateString() } " +
                    $" <br /> Starts: {EventHelpers.GetStandardTimeFromMilitaryTime(model.StartTime)} Ends: {EventHelpers.GetStandardTimeFromMilitaryTime(model.EndTime)} " +
                    $" <br /> Category: {model.Category} " +
                    $" <br /> Sandwiches: { model.SandwichSlots }" +
                    $" <br /> Side Dishes: {model.SideDishSlots }" +
                    $" <br /> Main Dishes: {model.MainDishSlots }" +
                    $" <br /> Desserts: {model.DessertSlots}" +
                    $" <br /> Set Up: {model.SetUpSlots}" +
                    $" <br /> Servers: {model.ServeSlots}" +
                    $" <br /> Clean Up: {model.CleanUpSlots}" +
                    $" <br /> Notes: {model.Notes}"
            );
        }
    }
}
