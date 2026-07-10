public interface IUsersRepository
{
    public Task<UserEntity[]> GetAll(int ammount, int page, bool isTracking = false);
    public Task<UserEntity?> Get(string username, bool isTracking = false);
    public Task<UserEntity?> Get(int userId, bool isTracking = false);
    public Task<bool> Exists(string username);
    public Task<bool> Exists(int userId);
    public Task Post(UserEntity user);
    public Task Update(UserEntity user);
    public Task Delete(int userId);
}