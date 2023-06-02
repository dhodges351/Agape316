using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Agape316.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;       
        }

        public IActionResult Index()
        {                   
            return View();
        }

        public IActionResult GetEventListPartialView()
        {
            return PartialView("_EventListPartialView");
        }

        public IActionResult Privacy()
        {
            return View();
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