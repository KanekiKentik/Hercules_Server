using System.Data.Common;
using Microsoft.EntityFrameworkCore;

public class MuscleGroupsRepository : IMuscleGroupsRepository
{
    private HerculesContext _db;
    public MuscleGroupsRepository(HerculesContext context) => _db = context;
    public async Task<MuscleGroupEntity[]> GetAll(bool isTracking = false)
    {
        IQueryable<MuscleGroupEntity> query = _db.MuscleGroups;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .ToArrayAsync();
    }
}