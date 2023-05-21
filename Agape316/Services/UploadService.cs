using Agape316.Data;

namespace Agape316.Services
{
    public class UploadService : IUpload
    {
        public UploadService()
        {
        }

        public async Task UploadImage(IFormFile formFile)
        {
//            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
//#if DEBUG
//            folderName = Path.Combine("ClientApp\\src\\assets", fileName.Contains("pdf") ? "exhibits" : "images");
//#else
//                        folderName = Path.Combine("ClientApp\\dist\\assets", fileName.Contains("pdf") ? "exhibits" : "images");
//#endif
//            pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
//            var fullPath = Path.Combine(pathToSave, fileName);
//            dbPath += Path.Combine(folderName, fileName) + ",";

            //using (var stream = new FileStream(fullPath, FileMode.Create))
            //{
            //    file.CopyTo(stream);
            //}

            string userId = string.Empty;
            Uri uri = new Uri("/uploads/");
            //await _userService.SetProfileImage(userId, uri);
        }
    }
}
