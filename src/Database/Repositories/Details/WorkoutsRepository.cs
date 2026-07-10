using Microsoft.EntityFrameworkCore;

public class WorkoutsRepository : IWorkoutsRepository
{
    private HerculesContext _db;
    public WorkoutsRepository(HerculesContext context) => _db = context;
    public async Task<WorkoutEntity?> Get(int workoutId, bool isTracking = false)
    {
        IQueryable<WorkoutEntity> query = _db.Workouts;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Include(w => w.SessionExercises)
                .ThenInclude(s => s.Sets)
            .FirstOrDefaultAsync(w => w.Id == workoutId);
    }
    public async Task<WorkoutEntity[]> GetAllFiltered(int userId, DateTimeOffset dateFrom = default, DateTimeOffset dateTo = default, bool isTracking = false)
    {
        IQueryable<WorkoutEntity> query = _db.Workouts
                        .Where(w => w.UserId == userId)
                        .Include(w => w.SessionExercises)
                            .ThenInclude(s => s.Sets);

        if (!isTracking)
            query = query.AsNoTracking();

        if (dateFrom != default) 
            query = query.Where(w => w.StartTime >= dateFrom);

        if (dateTo != default)
            query = query.Where(w => w.StartTime <= dateTo);

        return await query.ToArrayAsync();
    }
    public async Task<WorkoutEntity[]> GetAll(int userId, bool isTracking = false)
    {
        IQueryable<WorkoutEntity> query = _db.Workouts;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Where(w => w.UserId == userId)
            .Include(w => w.SessionExercises)
                .ThenInclude(s => s.Sets)
            .ToArrayAsync();
    }
    public async Task<WorkoutEntity[]> GetAll(int userId, int ammount, int page, bool isTracking = false)
    {
        IQueryable<WorkoutEntity> query = _db.Workouts;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Where(w => w.UserId == userId)
            .Include(w => w.SessionExercises)
                .ThenInclude(s => s.Sets)
            .ToArrayAsync();
    }
    public async Task Post(WorkoutEntity workout)
    {
        _db.Workouts.Add(workout);
        await _db.SaveChangesAsync();
    }
    public async Task Update(WorkoutEntity updated)
    {
        _db.Workouts.Update(updated);
        await _db.SaveChangesAsync();
    }
    public async Task Delete(int workoutId)
    {
        await _db.Workouts
                .Where(w => w.Id == workoutId)
                .ExecuteDeleteAsync();
    }
}