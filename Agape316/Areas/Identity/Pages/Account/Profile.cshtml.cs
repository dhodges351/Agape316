// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Agape316.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Agape316.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace Agape316.Areas.Identity.Pages.Account
{
    public class ProfileModel : PageModel
    {        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProfileModel> _logger;
        private readonly IApplicationUser _appUserService;
        private readonly IUpload _uploadService;
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime MemberSince { get; set; }
        public IFormFile ImageUpload { get; set; }

        public ProfileModel(
            UserManager<ApplicationUser> userManager,
            ILogger<ProfileModel> logger,
            IUpload uploadService,
            IApplicationUser appUserService)
        {
            _userManager = userManager;
            _logger = logger;
            _uploadService = uploadService;
            _appUserService = appUserService;   
        }
		
		public async Task OnGetAsync(string returnUrl = null)
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
				}
            }            
        }

        public async Task<IActionResult> OnPost(IFormFile formFile, string returnUrl = null)
        {
            await _uploadService.UploadImage(formFile);

            _logger.LogInformation("User profile image saved.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
