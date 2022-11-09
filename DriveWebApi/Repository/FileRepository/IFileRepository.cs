using DriveWebApi.Models;

namespace DriverWebApi.Services.File
{
    public interface IFileRepository
    {
        Task<List<Image>> GetImages(int userId);
        Task<Image> GetImage(string url);
        Image Create(IFormFile file, int userId);
        Task<bool> Delete(int id, string login);
    }
}
