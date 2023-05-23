using Agape316.Areas.Identity.Pages.Account;
using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;

namespace Agape316.Controllers
{
    public class EventController : Controller
    {
        private readonly IEvent _eventService;
        private readonly IUpload _uploadService;

        public EventController(IEvent eventService, IUpload uploadService)
        {
            _eventService = eventService;
            _uploadService = uploadService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEvent(CreateEventModel model)
        {            
            if (model.ImageUrl != null)
            {
                //_uploadService.SaveProfileImage()
            }

            var agapeEvent = new Event
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                EventDate = DateTime.Now,
                ImageUrl = model.ImageUrl,
                Location = model.Location,
                CategoryId = model.CategoryId,
                ContactEmail = model.ContactEmail,
                Notes = model.Notes,
            };

            await _eventService.Create(agapeEvent);

            return RedirectToAction("Index", "Home");
        }
    }
}
