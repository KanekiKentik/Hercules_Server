public interface ISessionExercisesRepository
{
    public Task<SessionExerciseEntity?> Get(int id, bool isTracking = false);
    public Task<int?> GetWorkoutId(int id);
    public Task Delete(int sessionExerciseId);
}