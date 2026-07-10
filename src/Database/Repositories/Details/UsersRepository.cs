using Microsoft.EntityFrameworkCore;

public class UsersRepository : IUsersRepository
{
    private HerculesContext _db;
    public UsersRepository(HerculesContext context) => _db = context;
    public async Task<UserEntity?> Get(string username, bool isTracking = false)
    {
        IQueryable<UserEntity> query = _db.Users;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();
    }

    public async Task<UserEntity?> Get(int userId, bool isTracking = false)
    {
        IQueryable<UserEntity> query = _db.Users;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<UserEntity[]> GetAll(bool isTracking = false)
    {
        IQueryable<UserEntity> query = _db.Users;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .ToArrayAsync();
    }
    public async Task<UserEntity[]> GetAll(int ammount, int page, bool isTracking = false)
    {
        IQueryable<UserEntity> query = _db.Users;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Skip(ammount * (page - 1))
            .Take(ammount)
            .ToArrayAsync();
    }

    public async Task<bool> Exists(string username)
    {
        var found = await _db.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Username == username);

        return found != null;
    }
    public async Task<bool> Exists(int userId)
    {
        var found = await _db.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Id == userId);

        return found != null;
    }
    public async Task Post(UserEntity user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }
    public async Task Update(UserEntity user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }
    public async Task Delete(int userId)
    {
        await _db.Users
                .Where(u => u.Id == userId)
                .ExecuteDeleteAsync();
    }
}