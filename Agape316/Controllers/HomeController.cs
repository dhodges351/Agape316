using Agape316.Data;
using Agape316.Models;
using Agape316.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Agape316.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEvent _eventService;

        public HomeController(ILogger<HomeController> logger, IEvent eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            var model = new EventModel(_eventService);
            ViewBag.EventModel = model; 
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}