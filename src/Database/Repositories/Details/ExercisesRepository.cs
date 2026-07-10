using Microsoft.EntityFrameworkCore;

public class ExercisesRepository : IExercisesRepository
{
    private HerculesContext _db;
    public ExercisesRepository(HerculesContext context) => _db = context;
    public async Task<ExerciseEntity?> Get(int id, bool isTracking = false)
    {
        IQueryable<ExerciseEntity> query = _db.Exercises;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Include(e => e.Muscles)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<ExerciseEntity[]> GetAll(bool isTracking = false)
    {
        IQueryable<ExerciseEntity> query = _db.Exercises;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Include(e => e.Muscles)
            .ToArrayAsync();
    }

    public async Task<ExerciseEntity[]> GetAll(int[] ids, bool isTracking = false)
    {
        IQueryable<ExerciseEntity> query = _db.Exercises;

        if (!isTracking)
            query.AsNoTracking();

        return await query
            .Where(e => ids.Contains(e.Id))
            .Include(e => e.Muscles)
            .ToArrayAsync();
    }

    public async Task<ExerciseEntity[]> GetFiltered(string name = null!, string[] muscleGroups = null!, bool isTracking = false)
    {
        IQueryable<ExerciseEntity> querry = _db.Exercises.Include(e => e.Muscles);

        if(!isTracking)
            querry = querry.AsNoTracking();

        if(!string.IsNullOrEmpty(name)) 
            querry = querry.Where(e => e.Name.Contains(name));

        if(muscleGroups is { Length: > 0 }) 
            querry = querry.Where(e => muscleGroups.All(m => e.Muscles.Select(m => m.Name).Contains(m)));

        return await querry.ToArrayAsync();
    }
}