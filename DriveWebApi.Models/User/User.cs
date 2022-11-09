using System.ComponentModel.DataAnnotations;

namespace DriveWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public List<Image> Images { get; set; }
    }
}