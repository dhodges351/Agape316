using Agape316.Data;
using Agape316.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agape316.Controllers
{
	public class AdminController : Controller
	{
        private readonly IApplicationUser _appUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IApplicationUser appUserService, UserManager<ApplicationUser> userManager)
        {
            _appUserService = appUserService;
            _userManager = userManager;
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
            var model = new ManageUsersViewModel(_appUserService, _userManager);
            if (model.Users != null && model.Users.Any())
            {
                foreach (var user in model.Users)
                {
                    var userRoles = _userManager.GetRolesAsync(user).Result;
                    if (userRoles != null && userRoles.Count() > 0)
                    {
                        user.IsAdmin = userRoles.FirstOrDefault() == "Admin";
                    }
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeUserAdmin(string email)
        {
            var model = new ManageUsersViewModel(_appUserService, _userManager);
            await model.MakeUserAdminAsync(email, _userManager);
            return Json("Done");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveUserAdmin(string email)
        {
            var model = new ManageUsersViewModel(_appUserService, _userManager);
            await model.RemoveUserAdminAsync(email, _userManager);
            return Json("Done");
        }
    }
}
