using Agape316.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;
using Agape316.Data;

namespace Agape316.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly INotyfService _toastNotification;
        private readonly IEvent _eventService;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, INotyfService toastNotification, IEvent eventService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _toastNotification = toastNotification; 
            _eventService = eventService;
        }

        public IActionResult Index(string searchQuery)
        {
            var model = new HomeIndexModel(_eventService, searchQuery);
            if (string.IsNullOrEmpty(searchQuery))
            {
                model.Events = null;
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUsAsync(HomeIndexModel model)
        {
            await model.SendContactUsEmail(_emailSender, model);
            _toastNotification.Success("Thank you for contacting Agape 316 Ministry!", 5);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            // Get the details of the exception that occurred
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                // Get which route the exception occurred at
                string routeWhereExceptionOccurred = exceptionFeature.Path;

                // Get the exception that occurred
                Exception exceptionThatOccurred = exceptionFeature.Error;

                _logger.LogError(exceptionThatOccurred.Message + "\r\n" + exceptionThatOccurred.InnerException + "\r\n" + exceptionThatOccurred.StackTrace);
            }

            return View();
        }
    }
}