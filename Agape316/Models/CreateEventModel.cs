using Agape316.Data;
using Agape316.Enums;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Collections;

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
        public DateTime EventDate { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: HH:mm:ss}")] 
        public string? StartTime { get; set; }
        
        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: HH:mm:ss}")] 
        public string? EndTime { get; set; }

        public int CategoryId { get; set; }

        IEnumerable<Category> Categories { get; set; } = new List<Category>();

        [Required]        
        [StringLength(50, ErrorMessage = "Category is required")]
        public string Category { get; set; }

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
    }
}
