public interface IMuscleGroupsRepository
{
    public Task<MuscleGroupEntity[]> GetAll(bool isTracking = false);
}