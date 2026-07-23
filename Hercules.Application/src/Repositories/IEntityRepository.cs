public interface IEntityRepository<T> where T : class, IEntityBase
{
    public Task<T?> Get(int id, bool isTracking = false);
    public Task<T[]> Get(int[] ids, bool isTracking = false);
    public Task<T[]> GetAll(int amount, int page = 0, bool isTracking = false);
    public Task Post(T entity);
    //What if I try to update unexistent entity?
    public Task<Result> Update(T entity);
    public Task<Result> Delete(int id);
    public Task<Result> Delete(int[] ids);
}