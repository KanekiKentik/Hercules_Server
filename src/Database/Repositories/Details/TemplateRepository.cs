using Microsoft.EntityFrameworkCore;

public class TemplatesRepository : ITemplatesRepository
{
    private HerculesContext _db;
    public TemplatesRepository(HerculesContext context) => _db = context;
    public async Task<TemplateEntity?> Get(int templateId, bool isTracking = false)
    {
        IQueryable<TemplateEntity> query = _db.Templates;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Where(t => t.Id == templateId)
            .Include(t => t.Exercises)
            .FirstOrDefaultAsync();
    }
    public async Task<TemplateEntity[]> GetAll(int userId, bool isTracking = false)
    {
        IQueryable<TemplateEntity> query = _db.Templates;

        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Where(t => t.UserId == userId)
            .Include(t => t.Exercises)
            .ToArrayAsync();
    }
    public async Task Post(TemplateEntity template)
    {
        _db.Templates.Add(template);
        await _db.SaveChangesAsync();
    }
    public async Task Update(TemplateEntity template)
    {
        _db.Templates.Update(template);
        await _db.SaveChangesAsync();
    }
    public async Task Delete(int templateId)
    {
        await _db.Templates
                .Where(t => t.Id == templateId)
                .ExecuteDeleteAsync();
    }
}