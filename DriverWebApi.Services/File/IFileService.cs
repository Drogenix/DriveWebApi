using Microsoft.AspNetCore.Http;

namespace DriverWebApi.Services.File
{
    public interface IFileRepository
    {
        bool Create(IFormFile file);

        bool Delete(int id, string login);
    }
}
