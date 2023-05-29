using Agape316.Areas.Identity.Pages.Account;
using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace Agape316.Controllers
{
    public class EventDishController : Controller
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public EventDishController(IEvent eventService, IEventDish eventDishService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
            _environment = environment;
        }
        
        public IActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OnGetCallEventDishViewComponent(int id)
        {
            return ViewComponent("EventDish", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditEventDish(EventDishModel model)
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

            model.SaveEventDish(model, fileName, _eventDishService);

            return RedirectToAction("Index", "Home");
        }
    }
}
