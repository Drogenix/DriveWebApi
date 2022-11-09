using DriveWebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _repository;

        public StatisticsController(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<object>> GetAppStatistics()
        {
            var imagesCount = _repository.GetImagesCount();

            var usersCount = _repository.GetUsersCount();

            var totalVisits = _repository.GetTotalVisitsCount();

            _repository.AddVisit();

            return new { ImagesCount = imagesCount, UsersCount = usersCount, TotalVisits = totalVisits };
        }

    }
}
