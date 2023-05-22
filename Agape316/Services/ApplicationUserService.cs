using Agape316.Data;

namespace Agape316.Services
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;
        
        public ApplicationUserService(ApplicationDbContext context) 
        { 
            _context= context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id == id);
        }

        public ApplicationUser GetByUsername(string userName)
        {
            return GetAll().FirstOrDefault(user => user.UserName == userName);
        }

        public async Task SetProfileImage(string id, string pathToImage)
        {
            var user = GetById(id);
            user.ProfileImageUrl = pathToImage;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
