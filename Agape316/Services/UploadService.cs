using Agape316.Data;

namespace Agape316.Services
{
    public class UploadService : IUpload
    {
		private readonly IApplicationUser _appUserService;
		public UploadService(IApplicationUser appUserService)
        {
			_appUserService = appUserService;
		}

        public async Task SaveProfileImage(string userId, string fileName)
        {
            string pathToImage = "/upload/" + fileName;
            await _appUserService.SetProfileImage(userId, pathToImage);
        }
    }
}
