public interface IWorkoutsRepository : IEntityRepository<WorkoutEntity>
{
    public Task<WorkoutEntity[]> GetAll(int userId, int ammount, int page = 0, bool isTracking = false);
    public Task<WorkoutEntity[]> GetAllFiltered(int userId, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, bool isTracking = false);
}