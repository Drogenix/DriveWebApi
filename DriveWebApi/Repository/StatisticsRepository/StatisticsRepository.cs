using DriveWebApi.Data;

namespace DriveWebApi.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {

        private readonly ApplicationDbContext _context;

        public StatisticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddVisit()
        {
            _context.Visits.First().TotalVisits += 1;

            _context.SaveChanges();
        }

        public int GetImagesCount()
        {
            return _context.Images.Count();
        }

        public int GetTotalVisitsCount()
        {
            return _context.Visits.First().TotalVisits;
        }

        public int GetUsersCount()
        {
            return _context.Users.Count();
        }
    }
}
