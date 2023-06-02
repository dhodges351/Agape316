using Agape316.Data;
using Agape316.Enums;
using Ganss.Xss;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Agape316.Models
{
    public class EventDishModel
    {
        private readonly IEventDish _eventDishService;
        private readonly IEvent _eventService;
        public string note { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public string location { get; private set; }
        public string contactEmail { get; private set; }        
        public string imageUrl { get; private set; }
        public string category { get; private set; }

        public EventDishModel()
        {
        }

        public EventDishModel(IEvent eventService, IEventDish eventDishService, int? eventId = null, int? id = null)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;

            if (!eventId.HasValue)
            {
                var eventDishes = _eventDishService.GetEventDishesByEventId(eventId ?? 0);                
            }
            else if (id.HasValue)
            {
                var eventDish = _eventDishService.GetById(id ?? 0);
                if (eventDish != null)
                {
                    Title = eventDish.Title;
                    Description = eventDish.Description;
                    ImageUrl = eventDish.ImageUrl;
                    Created = DateTime.Now;
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
        public string Notes
        {
            get => note;
            set => note = new HtmlSanitizer().Sanitize(value);
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
        public string ImageUrl
        {
            get => imageUrl;
            set => imageUrl = new HtmlSanitizer().Sanitize(value);
        }
        public string Category
        {
            get => category;
            set => category = new HtmlSanitizer().Sanitize(value);
        }
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
        public bool HasInvalidSlot { get; set; } = false;
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

		public bool ValidateEventSlot(IEvent _eventService, IEventDish _eventDishService, int eventId, string slotName, int slotQuantity)
		{
			bool retVal = true;

			var agapeEvent = _eventService.GetByEventId(eventId);
			var eventDishes = _eventDishService.GetEventDishesByEventId(eventId);
			int sandwichSlotsUsed = 0;
			int sideDishSlotsUsed = 0;
			int mainDishSlotsUsed = 0;
			int dessertSlotsUsed = 0;
			int setUpSlotsUsed = 0;
			int serveSlotsUsed = 0;
			int cleanUpSlotsUsed = 0;

			if (eventDishes != null && eventDishes.Any())
			{
				foreach (var eventDish in eventDishes)
				{
					sandwichSlotsUsed += eventDish.SandwichSlot ?? 0;
					sideDishSlotsUsed += eventDish.SideDishSlot ?? 0;
					mainDishSlotsUsed += eventDish.MainDishSlot ?? 0;
					dessertSlotsUsed += eventDish.DessertSlot ?? 0;
					setUpSlotsUsed += eventDish.SetUpSlot ?? 0;
					serveSlotsUsed += eventDish.ServeSlot ?? 0;
					cleanUpSlotsUsed += eventDish.CleanUpSlot ?? 0;
				}
			}

			if (agapeEvent != null)
			{
				switch (slotName)
				{
					case "SandwichSlot":
						retVal = (sandwichSlotsUsed + slotQuantity) > agapeEvent.SandwichSlots;
						break;
					case "SideDishSlot":
						retVal = (sideDishSlotsUsed + slotQuantity) > agapeEvent.SideDishSlots;
						break;
					case "MainDishSlot":
						retVal = (mainDishSlotsUsed + slotQuantity) > agapeEvent.MainDishSlots;
						break;
					case "DessertSlot":
						retVal = (dessertSlotsUsed + slotQuantity) > agapeEvent.DessertSlots;
						break;
					case "SetUpSlot":
						retVal = (setUpSlotsUsed + slotQuantity) > agapeEvent.SetUpSlots;
						break;
					case "ServeSlot":
						retVal = (serveSlotsUsed + slotQuantity) > agapeEvent.ServeSlots;
						break;
					case "CleanUpSlot":
						retVal = (cleanUpSlotsUsed + slotQuantity) > agapeEvent.CleanUpSlots;
						break;
				}
			}

			return retVal;
		}

		public void SaveEventDish(EventDishModel model, string fileName, IEventDish _eventDishService)
        {
            if (!model.Id.HasValue)
            {
                var eventDish = new EventDish
                {
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    ImageUrl = System.Web.HttpUtility.HtmlEncode("/upload/" + fileName),
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
                    EventId = model.EventId ?? 0
                };
                _eventDishService.Create(eventDish);
            }
            else
            {
                var eventDish = _eventDishService.GetById(model.Id.Value);
                eventDish.Title = model.Title;
                eventDish.Description = model.Description;
                eventDish.Created = DateTime.Now;
                eventDish.ImageUrl = System.Web.HttpUtility.HtmlEncode("/upload/" + fileName);
                eventDish.Category = model.Category;
                eventDish.Notes = model.Notes;
                eventDish.SandwichSlot = model.SandwichSlot;
                eventDish.SideDishSlot = model.SideDishSlot;
                eventDish.MainDishSlot = model.MainDishSlot;
                eventDish.DessertSlot = model.DessertSlot;
                eventDish.SetUpSlot = model.SetUpSlot;
                eventDish.ServeSlot = model.ServeSlot;
                eventDish.CleanUpSlot = model.CleanUpSlot;
                eventDish.EventId = model.EventId ?? 0;

                _eventDishService.UpdateEventDish(eventDish);
            }
        }
    }
}
