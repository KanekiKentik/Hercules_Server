public interface IExercisesRepository
{
    public Task<ExerciseEntity?> Get(int id, bool isTracking = false);
    public Task<ExerciseEntity[]> GetAll(bool isTracking = false);
    public Task<ExerciseEntity[]> GetAll(int[] ids, bool isTracking = false);
    public Task<ExerciseEntity[]> GetFiltered(string name = null!, string[] muscleGroups = null!, bool isTracking = false);
}