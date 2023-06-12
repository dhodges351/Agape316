using Agape316.Data;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Agape316.Models
{
    public class ManageUsersViewModel
    {
        private readonly IApplicationUser _appUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersViewModel(IApplicationUser appUserService, UserManager<ApplicationUser> userManager)
        {           
            _appUserService = appUserService;
            _userManager = userManager;
            Users = _appUserService.GetAll().ToList();            
        }

        public IFormFile ImageUpload { get; set; }

        public List<ApplicationUser>? Users { get; set; }

        public async Task MakeUserAdminAsync(string userName, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                try
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    throw;
                }                
            }            
        }

        public async Task RemoveUserAdminAsync(string userName, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                try
                {
                    await userManager.RemoveFromRoleAsync(user, "Admin");
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    throw;
                }
            }
        }
    }
}
