using Agape316.Data;
using Agape316.Models;
using Agape316.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;

namespace Agape316.Controllers
{
    public class AdminController : Controller
	{
        private readonly IApplicationUser _appUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly INotyfService _toastNotification;
        private readonly IAgapeDocument _agapeDocumentService;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public AdminController(IApplicationUser appUserService, 
            UserManager<ApplicationUser> userManager,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment,
            INotyfService toastNotification,
            IAgapeDocument agapeDocumentService)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _environment = environment;
            _toastNotification = toastNotification;
            _agapeDocumentService = agapeDocumentService;
        }

        [Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
            ViewData["RootPath"] = _environment.WebRootPath + "\\upload";
            var model = new AdminViewModel(_agapeDocumentService);
            return View(model);
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEditDocument(AdminViewModel model)
        {
            if (Upload != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var fileName = Upload.FileName;

                    var file = Path.Combine(_environment.WebRootPath, "upload", Upload.FileName);

                    using var fileStream = new FileStream(file, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);

                    model.NameOfFile = fileName;
                    await model.SaveDocument(model, _agapeDocumentService);
                }
            }

           return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Download(string fileName)
        {
            var filepath = Path.Combine(_environment.WebRootPath, "upload", fileName);
            var mimeType = Agape316.Helpers.MiscHelpers.GetMimeTypeForFileExtension(filepath);
            return File(System.IO.File.ReadAllBytes(filepath), mimeType, System.IO.Path.GetFileName(filepath));
        }
    }
}
