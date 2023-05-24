﻿using Agape316.Areas.Identity.Pages.Account;
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
        private readonly IApplicationUser _appUserService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public EventController(IEvent eventService, IUpload uploadService, 
            IApplicationUser appUserService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _eventService = eventService;
            _uploadService = uploadService;
            _appUserService = appUserService;
            _environment = environment;
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
            var fileName = string.Empty;

            if (Upload != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = _appUserService.GetByUsername(User.Identity.Name);

                    fileName = Upload.FileName;

                    var file = Path.Combine(_environment.WebRootPath, "upload", Upload.FileName);

                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }

                    await _uploadService.SaveProfileImage(user?.Id, fileName);
                }
            }

            var agapeEvent = new Event
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                EventDate = DateTime.Now,
                ImageUrl = "/upload/" + fileName,
                Location = model.Location,
                CategoryId = model.GetCategoryId(model.Category),
                ContactEmail = model.ContactEmail,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Notes = model.Notes,
            };

            await _eventService.Create(agapeEvent);

            return RedirectToAction("Index", "Home");
        }
    }
}
