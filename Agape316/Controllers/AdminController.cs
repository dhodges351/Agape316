using Agape316.Data;
using Agape316.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agape316.Controllers
{
    public class AdminController : Controller
	{
        private readonly IApplicationUser _appUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly INotyfService _toastNotification;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public AdminController(IApplicationUser appUserService, 
            UserManager<ApplicationUser> userManager,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment,
            INotyfService toastNotification)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _environment = environment;
            _toastNotification = toastNotification;
        }

        [Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			return View();
		}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadDocumentAsync()
        {
            if (Upload != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var fileName = Upload.FileName;

                    var file = Path.Combine(_environment.WebRootPath, "upload", Upload.FileName);
                    ViewData["RootPath"] = _environment.WebRootPath + "\\upload";

                    using var fileStream = new FileStream(file, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                }
            }
            _toastNotification.Success("Thank you, your File has been Uploaded!", 5);
            return RedirectToAction("Index", "Admin");
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
