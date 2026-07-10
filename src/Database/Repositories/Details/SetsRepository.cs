using Microsoft.EntityFrameworkCore;

public class SetsRepository : ISetsRepository
{
    private HerculesContext _db;
    public SetsRepository(HerculesContext context) => _db = context;

    public async Task<SetEntity?> Get(int id, bool isTracking = false)
    {
        IQueryable<SetEntity> query = _db.Sets;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<int?> GetWorkoutId(int id)
    {
        var set = await _db.Sets
            .AsNoTracking()
            .Include(s => s.SessionExercise)
                .ThenInclude(s => s.Workout)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (set == null || set.SessionExercise == null) return null;
        return set.SessionExercise.WorkoutId;
    }
    public async Task Update(SetEntity set)
    {
        _db.Sets.Update(set);
        await _db.SaveChangesAsync();
    }
    public async Task Delete(int setId)
    {
        await _db.Sets
                .Where(s => s.Id == setId)
                .ExecuteDeleteAsync();
    }
}