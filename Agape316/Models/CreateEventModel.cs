using Agape316.Data;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class CreateEventModel
    {
        private readonly IEvent _eventService;

        public CreateEventModel() 
        { }

        public CreateEventModel(IEvent eventService)
        {
            _eventService = eventService;
            Categories = _eventService.GetAllCategories();
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: MM-dd-yyyy}")]

        [StringLength(10, ErrorMessage = "Event Date is required")]
        public DateTime EventDate { get; set; }

        public string? ImageUrl { get; set; }
              
        public int CategoryId { get; set; }

        IEnumerable<Category> Categories { get; set; } = new List<Category>();

        [Required]        
        [StringLength(50, ErrorMessage = "Category is required")]
        public int Category { get; set; }
    }
}
