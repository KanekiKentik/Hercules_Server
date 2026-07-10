public interface ITemplatesRepository
{
    public Task<TemplateEntity[]> GetAll(int userId, bool isTrackiking = false);
    public Task<TemplateEntity?> Get(int templateId, bool isTrackiking = false);
    public Task Post(TemplateEntity template);
    public Task Update(TemplateEntity template);
    public Task Delete(int templateId);
}