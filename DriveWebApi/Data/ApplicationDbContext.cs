using DriveWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<Visits> Visits { get; set; }
    }
}
