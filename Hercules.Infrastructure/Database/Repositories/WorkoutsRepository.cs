using Microsoft.EntityFrameworkCore;

internal class WorkoutsRepository : EntityRepository<WorkoutEntity>, IWorkoutsRepository
{
    public WorkoutsRepository(HerculesContext context, QueryBuilder<WorkoutEntity> builder) : base(context, builder) {}
    public async Task<WorkoutEntity[]> GetAllFiltered(int userId, DateTimeOffset? dateFrom = null, DateTimeOffset? dateTo = null, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);
        query = query.Where(w => w.UserId == userId);

        if (dateFrom.HasValue) 
            query = query.Where(w => w.StartTime >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(w => w.StartTime <= dateTo.Value);

        return await query.ToArrayAsync();
    }
    public async Task<WorkoutEntity[]> GetAll(int userId, int amount, int page = 0, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);

        return await query
            .Where(w => w.UserId == userId)
            .Skip(amount * page)
            .Take(amount)
            .ToArrayAsync();
    }
}