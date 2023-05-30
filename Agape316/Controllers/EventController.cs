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
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public EventController(IEvent eventService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _eventService = eventService;
            _environment = environment;
        }
        
        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]        
        public async Task<IActionResult> AddEditEvent(EventModel model)
        {
            var fileName = string.Empty;

            if (Upload != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    fileName = Upload.FileName;

                    var file = Path.Combine(_environment.WebRootPath, "upload", Upload.FileName);

                    using var fileStream = new FileStream(file, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    model.ImageUrl = "/upload/" + fileName;
                }
            }

            await model.SaveEvent(model, fileName, _eventService);

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult OnGetCallEventViewComponent(int id)
        {
            return ViewComponent("Event", new { id = id });
        }       
    }
}
