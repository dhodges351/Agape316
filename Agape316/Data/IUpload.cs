namespace Agape316.Data
{
    public interface IUpload
    {
        Task SaveProfileImage(string userId, string fileName);
	}
}
