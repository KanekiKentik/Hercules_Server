public interface ISetsRepository
{
    public Task<SetEntity?> Get(int id, bool isTracking = false);
    public Task<int?> GetWorkoutId(int id);
    public Task Update(SetEntity set);
    public Task Delete(int setId);
}