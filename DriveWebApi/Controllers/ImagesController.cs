using DriverWebApi.Services.File;
using DriveWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DriveWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public ImagesController(IFileRepository repository)
        {
            _fileRepository = repository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Image>>> GetUserImages()
        {
            var idClaim = User.FindFirstValue("id");

            var id = Convert.ToInt32(idClaim);

            var images = await _fileRepository.GetImages(id);

            if (images.Count > 0)
            {
                return images;
            }

            return NoContent();
        }

        [HttpGet("{url}")]
        public async Task<ActionResult> GetImage(string url)
        {
            var image = await _fileRepository.GetImage(url);

            if (image != null)
            {
                var localPath = @image.LocalPath;

                var img = await System.IO.File.ReadAllBytesAsync(localPath);

                var contentType = GetReponseContentType(localPath);

                return File(img, contentType);
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteImage(int id)
        {
            var userLogin = User.FindFirstValue("login");

            var isImageDeleted = await _fileRepository.Delete(id, userLogin);

            if (isImageDeleted)
            {
               return Ok();
            }
                
            return BadRequest();
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult> AddImage(IFormFile file)
        {
            var idClaim = User.FindFirst("id");

            var userId = Convert.ToInt32(idClaim?.Value);

            if(file != null && file.Length > 0)
            {
               var image = _fileRepository.Create(file, userId);

               if(image != null)
               {
                  return Ok(image);
               }

            }

            return BadRequest();
        }

        private string GetReponseContentType(string filePath)
        {
            var fileInfo = new FileInfo(filePath);

            return "image/" + fileInfo.Extension.Remove(0, 1);
        }

    }
}
