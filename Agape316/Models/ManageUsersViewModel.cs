using Agape316.Data;

namespace Agape316.Models
{
    public class ManageUsersViewModel
    {
        private readonly IApplicationUser _appUserService;

        public ManageUsersViewModel(IApplicationUser appUserService)
        {
            _appUserService = appUserService;
            Users = _appUserService.GetAll().ToList();
        }

        public IFormFile ImageUpload { get; set; }

        public List<ApplicationUser>? Users { get; set; }
    }
}
