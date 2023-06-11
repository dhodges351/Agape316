using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agape316.Controllers
{
	public class AdminController : Controller
	{
        private readonly IApplicationUser _appUserService;

        public AdminController(IApplicationUser appUserService)
        {
            _appUserService = appUserService;
        }

        [Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			return View();
		}

        [Authorize(Roles = "Admin")]
        public IActionResult UploadDocument()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageUsers()
        {
            var model = new ManageUsersViewModel(_appUserService);
            return View(model);
        }
    }
}
