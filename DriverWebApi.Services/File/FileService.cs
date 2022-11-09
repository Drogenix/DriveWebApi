

using Microsoft.AspNetCore.Http;

namespace DriverWebApi.Services.File
{
    public class FileRepository : IFileRepositorys
    {

        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Create(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, string login)
        {
            throw new NotImplementedException();
        }
    }
}
