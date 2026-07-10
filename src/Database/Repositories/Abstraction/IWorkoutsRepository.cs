public interface IWorkoutsRepository
{
    public Task<WorkoutEntity[]> GetAll(int userId, bool isTracking = false);
    public Task<WorkoutEntity[]> GetAllFiltered(int userId, DateTimeOffset dateFrom = default, DateTimeOffset dateTo = default, bool isTracking = false);
    public Task<WorkoutEntity[]> GetAll(int userId, int ammount, int page, bool isTracking = false);
    public Task<WorkoutEntity?> Get(int workoutId, bool isTracking = false);
    public Task Post(WorkoutEntity workout);
    public Task Update(WorkoutEntity updated);
    public Task Delete(int workoutId);
}