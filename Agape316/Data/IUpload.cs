namespace Agape316.Data
{
    public interface IUpload
    {
        Task UploadImage(IFormFile formFile);
    }
}
