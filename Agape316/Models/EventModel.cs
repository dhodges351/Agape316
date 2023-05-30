using Agape316.Data;
using Agape316.Enums;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class EventModel
    {
        private readonly IEvent _eventService;

        public EventModel()
        {
        }

        public EventModel(IEvent eventService, int? id = null)
        {
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
        }

        [Required]
        [StringLength(100, ErrorMessage = "Title is required")] 
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description is required")] 
        public string Description { get; set; }

        public string? Notes { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Location is required")] 
        public string Location { get; set; }

        [Required]
        [Display(Name = "Contact Email")]
        [StringLength(100, ErrorMessage = "Email is required")] 
        public string ContactEmail { get; set; } 

        public DateTime Created { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EventDate { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:  hh:mm:ss aa}")]
        public string? StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:  hh:mm:ss aa}")]
        public string? EndTime { get; set; }

        public int CategoryId { get; set; }
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
        public string Category { get; set; }

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

        public async Task SaveEvent(EventModel model, string fileName, IEvent _eventService)
        {
            if (!model.Id.HasValue)
            {
                var agapeEvent = new Event
                {                    
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    EventDate = model.EventDate,
                    ImageUrl = "/upload/" + fileName,
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
                _eventService.Create(agapeEvent);
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
        }
    }
}
