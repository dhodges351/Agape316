using Agape316.Data;
using Agape316.Enums;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class EventDishModel
    {
        private readonly IEventDish _eventDishService;
        private readonly IEvent _eventService;

        public EventDishModel()
        {
        }

        public EventDishModel(IEvent eventService, IEventDish eventDishService, int? eventId = null, int? id = null)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;

            if (!eventId.HasValue)
            {
                var eventDishes = _eventDishService.GetEventDishesByEventDishId(eventId ?? 0);                
            }
            else if (id.HasValue)
            {
                var eventDish = _eventDishService.GetById(id ?? 0);
                if (eventDish != null)
                {
                    Title = eventDish.Title;
                    Description = eventDish.Description;
                    EventDate = eventDish.EventDate;
                    ImageUrl = eventDish.ImageUrl;
                    ContactEmail = eventDish.ContactEmail;
                    Notes = eventDish.Notes;
                    SandwichSlot = eventDish.SandwichSlot;
                    SideDishSlot = eventDish.SideDishSlot;
                    MainDishSlot = eventDish.MainDishSlot;
                    DessertSlot = eventDish.DessertSlot;
                    SetUpSlot = eventDish.SetUpSlot;
                    ServeSlot = eventDish.ServeSlot;
                    CleanUpSlot = eventDish.CleanUpSlot;
                    Category = eventDish.Category;
                }               
            }
        }

        [Required]
        [StringLength(100, ErrorMessage = "Title is required")] 
        public string? Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description is required")] 
        public string? Description { get; set; }

        public string? Notes { get; set; }       

        [Required]
        [Display(Name = "Contact Email")]
        [StringLength(100, ErrorMessage = "Email is required")] 
        public string? ContactEmail { get; set; } 

        public DateTime Created { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EventDate { get; set; }

        public string? ImageUrl { get; set; }        
        public string? Category { get; set; }       
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please select an Event")] 
        public int? EventId { get; set; }
        public int? SandwichSlot { get; set; }
        public int? SideDishSlot { get; set; }
        public int? MainDishSlot { get; set; }
        public int? DessertSlot { get; set; }
        public int? SetUpSlot { get; set; }
        public int? ServeSlot { get; set; }
        public int? CleanUpSlot { get; set; } 
        public IEnumerable<EventDish> EventDishes { get; set; }
        public Event AgapeEvent { get; set; }
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

        public void SaveEventDish(EventDishModel model, string fileName, IEventDish _eventDishService)
        {
            AgapeEvent = _eventService.GetByEventId(model.Id ?? 0);

            if (!model.Id.HasValue)
            {
                var eventDish = new EventDish
                {
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    EventDate = model.EventDate,
                    ImageUrl = "/upload/" + fileName,
                    ContactEmail = model.ContactEmail,
                    Notes = model.Notes,
                    SandwichSlot = model.SandwichSlot,
                    SideDishSlot = model.SideDishSlot,
                    MainDishSlot = model.MainDishSlot,
                    DessertSlot = model.DessertSlot,
                    SetUpSlot = model.SetUpSlot,
                    ServeSlot = model.ServeSlot,
                    CleanUpSlot = model.CleanUpSlot,
                    Category = model.Category,
                };
                _eventDishService.Create(eventDish);
            }
            else
            {
                var eventDish = _eventDishService.GetById(model.Id.Value);
                eventDish.Title = model.Title;
                eventDish.Description = model.Description;
                eventDish.Created = DateTime.Now;
                eventDish.EventDate = model.EventDate;
                eventDish.ImageUrl = model.ImageUrl;
                eventDish.Category = model.GetCategoryName(AgapeEvent.CategoryId);
                eventDish.Notes = model.Notes;
                eventDish.SandwichSlot = model.SandwichSlot;
                eventDish.SideDishSlot = model.SideDishSlot;
                eventDish.MainDishSlot = model.MainDishSlot;
                eventDish.DessertSlot = model.DessertSlot;
                eventDish.SetUpSlot = model.SetUpSlot;
                eventDish.ServeSlot = model.ServeSlot;
                eventDish.CleanUpSlot = model.CleanUpSlot;

                _eventDishService.UpdateEventDish(eventDish);
            }
        }
    }
}
