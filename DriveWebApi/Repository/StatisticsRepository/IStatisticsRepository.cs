namespace DriveWebApi.Repository
{
    public interface IStatisticsRepository
    {
        int GetUsersCount();

        int GetImagesCount();

        int GetTotalVisitsCount();

        void AddVisit();
    }
}
