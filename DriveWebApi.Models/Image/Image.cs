
namespace DriveWebApi.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocalPath { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
    }
}
