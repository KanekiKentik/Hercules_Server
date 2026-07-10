using Microsoft.EntityFrameworkCore;

public class SessionExercisesRepository : ISessionExercisesRepository
{
    private HerculesContext _db;
    public SessionExercisesRepository(HerculesContext context) => _db = context;
    public async Task<SessionExerciseEntity?> Get(int id, bool isTracking = false)
    {
        IQueryable<SessionExerciseEntity> query = _db.SessionExercises;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Include(s => s.Sets)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<int?> GetWorkoutId(int id)
    {
        var sessionExercise = await _db.SessionExercises
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sessionExercise == null) return null;
        return sessionExercise.WorkoutId;
    }
    public async Task Delete(int sessionExerciseId)
    {
        await _db.SessionExercises
                .Where(s => s.Id == sessionExerciseId)
                .ExecuteDeleteAsync();
    }
}