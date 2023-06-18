using Agape316.Data;
using Agape316.Helpers;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;

namespace Agape316.Models
{
    public class AdminViewModel
    {
        public string? _notes { get; private set; }
        public string? _title { get; private set; }
        public string? _description { get; private set; }
        public string? _author { get; private set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100, ErrorMessage = "Title is required")]
        public string? Title
        {
            get => _title;
            set => _title = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(200, ErrorMessage = "Description is required")]
        public string? Description
        {
            get => _description;
            set => _description = new HtmlSanitizer().Sanitize(value);
        }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string? Notes
        {
            get => _notes;
            set => _notes = new HtmlSanitizer().Sanitize(value == null ? "" : value);
        }

        [Required]
        [StringLength(100, ErrorMessage = "Author is required")]
        public string? Author
        {
            get => _author;
            set => _author = new HtmlSanitizer().Sanitize(value);
        }

        [Required]
        [StringLength(100, ErrorMessage = "File Type is required")]
        public string? FileType { get; set; }


        public async Task SaveDocument(AdminViewModel model, IAgapeDocument _agapeDocumentService)
        {
            //if (!model.Id.HasValue)
            //{
            //    var agapeEvent = new Event
            //    {
            //        Title = model.Title,
            //        Description = model.Description,
            //        Created = DateTime.Now,
            //        EventDate = model.EventDate,
            //        ImageUrl = model.ImageUrl,
            //        Location = model.Location,
            //        CategoryId = model.GetCategoryId(model.Category),
            //        ContactEmail = model.ContactEmail,
            //        StartTime = model.StartTime,
            //        EndTime = model.EndTime,
            //        Notes = model.Notes,
            //        SandwichSlots = model.SandwichSlots,
            //        SideDishSlots = model.SideDishSlots,
            //        MainDishSlots = model.MainDishSlots,
            //        DessertSlots = model.DessertSlots,
            //        SetUpSlots = model.SetUpSlots,
            //        ServeSlots = model.ServeSlots,
            //        CleanUpSlots = model.CleanUpSlots,
            //    };
            //    await _eventService.Create(agapeEvent);
            //}
            //else
            //{
            //    var agapeEvent = _eventService.GetById(model.Id.Value);
            //    agapeEvent.Title = model.Title;
            //    agapeEvent.Description = model.Description;
            //    agapeEvent.Created = DateTime.Now;
            //    agapeEvent.EventDate = model.EventDate;
            //    agapeEvent.ImageUrl = model.ImageUrl;
            //    agapeEvent.Location = model.Location;
            //    agapeEvent.CategoryId = model.GetCategoryId(model.Category);
            //    agapeEvent.ContactEmail = model.ContactEmail;
            //    agapeEvent.StartTime = model.StartTime;
            //    agapeEvent.EndTime = model.EndTime;
            //    agapeEvent.Notes = model.Notes;
            //    agapeEvent.SandwichSlots = model.SandwichSlots;
            //    agapeEvent.SideDishSlots = model.SideDishSlots;
            //    agapeEvent.MainDishSlots = model.MainDishSlots;
            //    agapeEvent.DessertSlots = model.DessertSlots;
            //    agapeEvent.SetUpSlots = model.SetUpSlots;
            //    agapeEvent.ServeSlots = model.ServeSlots;
            //    agapeEvent.CleanUpSlots = model.CleanUpSlots;

            //    _eventService.UpdateEvent(agapeEvent);
            //}
        }
    }
}