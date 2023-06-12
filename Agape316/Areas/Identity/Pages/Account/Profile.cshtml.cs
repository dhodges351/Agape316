// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable
using Agape316.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Agape316.Areas.Identity.Pages.Account
{
	public class ProfileModel : PageModel
    {        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProfileModel> _logger;
        private readonly IApplicationUser _appUserService;
        private readonly IUpload _uploadService;
		private readonly IHostingEnvironment _environment;

		public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
		public string ProfileImageUrl { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime MemberSince { get; set; }

		[BindProperty]
		public IFormFile Upload { get; set; }

		[BindProperty]
		public string PhoneNumber { get; set; }

		public ProfileModel(
            UserManager<ApplicationUser> userManager,
            ILogger<ProfileModel> logger,
            IUpload uploadService,
            IApplicationUser appUserService,
			IHostingEnvironment environment)
        {
            _userManager = userManager;
            _logger = logger;
            _uploadService = uploadService;
            _appUserService = appUserService;
			_environment = environment;
		}
		
		public async Task OnGetAsync()
        {		
			if (User.Identity.IsAuthenticated)
            {
                var user = _appUserService.GetByUsername(User.Identity.Name);
                if (user != null)
                {
                    FullName = user.FirstName + " " + user.LastName;
                    Email = user.Email;
					ProfileImageUrl = user.ProfileImageUrl;
                    IsAdmin = await _userManager.IsInRoleAsync(user, "ADMIN");
                    UserId = user.Id;
                    MemberSince = user.MemberSince;
                    PhoneNumber = user.PhoneNumber;
				}
            }            
        }

        public async Task<IActionResult> OnPostAsync()
        {
			if (User.Identity.IsAuthenticated)
			{
				var user = _appUserService.GetByUsername(User.Identity.Name);

                var fileName = Upload.FileName;

                user.PhoneNumber = PhoneNumber;

				var file = Path.Combine(_environment.WebRootPath, "upload", Upload.FileName);

				using (var fileStream = new FileStream(file, FileMode.Create))
				{
					await Upload.CopyToAsync(fileStream);
				}

				await _uploadService.SaveProfileImage(user?.Id, fileName);

				_logger.LogInformation("User profile image saved.");
			}		
            
            return RedirectToAction("Index", "Home");            
        }
    }
}
