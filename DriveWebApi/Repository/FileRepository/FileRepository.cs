using DriveWebApi.Data;
using DriveWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverWebApi.Services.File
{
    public class FileRepository : IFileRepository
    {

        private readonly ApplicationDbContext _context;

        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Image>> GetImages(int userId)
        {
            return await _context.Images.Where(item => item.UserId == userId).ToListAsync();
        }
        public async Task<Image> GetImage(string url)
        {
            return await _context.Images.FirstAsync(item => item.Url == url);
        }
        public Image? Create(IFormFile file, int userId)
        {
            try
            {
                var filePath = GenerateFilePath(file);

                WriteFile(filePath, file);

                var image = new Image()
                {
                    Name = file.FileName,
                    LocalPath = filePath,
                    Url = GenerateRandomString(20),
                    UserId = userId
                };

                _context.Images.Add(image);

                _context.SaveChanges();

                return image;

            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Delete(int id, string login)
        {
            var image = FindFirstAsync(login, id).Result;

            if(image !=null)
            {
                System.IO.File.Delete(image.LocalPath);

                _context.Images.Remove(image);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
        private async Task<Image?> FindFirstAsync(string userLogin, int imageId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == userLogin);

            if(user !=null)
            {
                return await _context.Images.FirstAsync(item => item.Id == imageId && user.Login == userLogin);
            }

            return null;
        }
        private void WriteFile(string filePath, IFormFile file)
        {
            using var stream = System.IO.File.Create(filePath);

            file.CopyTo(stream);

            stream.Close();
        }
        private string GenerateFilePath(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            var randomName = GenerateRandomString(20);

            return @"images/" + randomName + fileExtension;
        }
        private string GenerateRandomString(int size)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "0123456789" + "abcdefghijklmnopqrstuvwxyz";

            var randomString = "";

            var randomizer = new Random();

            for (int i = 0; i < size; i++)
            {
                randomString += characters[randomizer.Next(0, characters.Length)];
            }

            return randomString;
        }
    }
}
