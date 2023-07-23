using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Agape316.Controllers
{
    public class EventController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IEvent _eventService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly INotyfService _toastNotification;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public EventController(IEmailSender emailSender, IEvent eventService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, 
            INotyfService toastNotification)
        {
            _emailSender = emailSender;
            _eventService = eventService;
            _environment = environment;
            _toastNotification = toastNotification;
        }
        
        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]        
        public async Task<IActionResult> AddEditEvent(EventModel model)
        { 
            if (Upload != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var fileName = Upload.FileName;

                    var file = Path.Combine(_environment.WebRootPath, "upload", Upload.FileName);

                    using var fileStream = new FileStream(file, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    model.ImageUrl = "/upload/" + fileName;
                }
            }

            if (User.Identity.IsAuthenticated)
            {
                 await model.SaveEvent(_emailSender, model, _eventService);
                _toastNotification.Success("Thank you, your Event has been saved!", 5);
            }

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult OnGetCallEventViewComponent(int id)
        {
            return ViewComponent("Event", new { id = id });
        }       
    }
}
