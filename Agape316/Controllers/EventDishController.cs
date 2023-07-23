using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Agape316.Controllers
{
    public class EventDishController : Controller
    {
        private readonly IEvent _eventService;
        private readonly IEventDish _eventDishService;
        private readonly IEmailSender _emailSender;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly INotyfService _toastNotification;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public EventDishController(IEvent eventService, IEventDish eventDishService,
            IEmailSender emailSender, 
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment,
            INotyfService toastNotification)
        {
            _eventService = eventService;
            _eventDishService = eventDishService;
            _environment = environment;
            _emailSender = emailSender;
            _toastNotification = toastNotification;
        }
        
        public IActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OnGetCallEventDishViewComponent(int eventId, int id)
        {
            return ViewComponent("EventDish", new { eventId = eventId, id = id });
        }

        [HttpGet]
        public IActionResult CheckEventSlot(int eventId, string slotName, int slotQuantity)
        {
            var eventDishModel = new EventDishModel();
            var isInvalidSlot = eventDishModel.ValidateEventSlot(_eventService, _eventDishService, eventId, slotName, slotQuantity);
            var model = new JsonMessage { Message = isInvalidSlot.ToString() };
            return new JsonResult(new { data = model });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditEventDish(EventDishModel model)
        {            
            if (model.HasInvalidSlot == true)
            {
                return RedirectToAction("Index", "Home");
            }

            string file = string.Empty;
            var fileName = string.Empty;      

            if (Upload != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    fileName = Upload.FileName;

                    file = Path.Combine(_environment.WebRootPath, "upload", Upload.FileName);

                    using var fileStream = new FileStream(file, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    model.ImageUrl = "/upload/" + fileName;
                }
            }

            if (User.Identity.IsAuthenticated)
            {
                await model.SaveEventDish(_emailSender, model, _eventDishService);
                _toastNotification.Success("Thank you, your Event Dish has been saved!", 5);
            }
               
            return RedirectToAction("Index", "Home");
        }

        public class JsonMessage
        {
            public string Message { get; set; }
        }
    }
}
