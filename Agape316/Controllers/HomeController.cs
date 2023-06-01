﻿using Agape316.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}