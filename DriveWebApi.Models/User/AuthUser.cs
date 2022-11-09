using System.ComponentModel.DataAnnotations;


namespace DriveWebApi.Models
{
    public class AuthUser
    {
        [Required]
        [MinLength(6)]
        public string Login { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(22)]
        public string Password { get; set; }
    }
}
