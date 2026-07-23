using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("/templates")]
public class TemplatesController : ControllerBase
{
    private readonly UsersService _uService;
    private readonly ITemplatesRepository _tRepo;
    private readonly IExercisesRepository _eRepo;

    public TemplatesController(UsersService uService, ITemplatesRepository tRepo, IExercisesRepository eRepo)
        => (_uService, _tRepo, _eRepo) = (uService, tRepo, eRepo);

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        Result<int> getResult = _uService.GetSelfId(User);
        if (getResult.IsFailure) 
            return this.HandleErrorResult(getResult);

        int userId = getResult.Value;
        var templates = await _tRepo.GetAll(userId);

        return Ok(templates
                .Select(t => t.ToResponse())
                .Where(r => r.IsSuccess)
                .Select(r => r.Value));
    }

    [HttpPost("post")]
    public async Task<IActionResult> Post([FromBody] TemplateRequest request)
    {
        var exercises = await _eRepo.Get(request.ExerciseIds, true);
        if (exercises is not { Length: > 0 }) return NotFound();

        Result<int> getResult = _uService.GetSelfId(User);
        if (getResult.IsFailure) 
            return this.HandleErrorResult(getResult);

        int userId = getResult.Value;
        TemplateEntity template = new (userId, request.Name, exercises);

        await _tRepo.Post(template);
        return Created();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int templateId)
    {
        Result<int> getResult = _uService.GetSelfId(User);
        if (getResult.IsFailure) 
            return this.HandleErrorResult(getResult);

        int userId = getResult.Value;

        var template = await _tRepo.Get(templateId);
        if (template == null) return NotFound();
        if (template.UserId != userId) return Forbid();

        await _tRepo.Delete(templateId);
        return NoContent();
    }

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromQuery] int templateId, [FromBody] TemplateRequest request)
    {
        Result<int> getResult = _uService.GetSelfId(User);
        if (getResult.IsFailure) 
            return this.HandleErrorResult(getResult);

        int userId = getResult.Value;

        var template = await _tRepo.Get(templateId, true);
        if (template == null) return NotFound("Template not found");
        if (template.UserId != userId) return Forbid();

        var exercises = await _eRepo.Get(request.ExerciseIds, true);
        if (exercises is not { Length: > 0 }) return NotFound("Template exercises not found");

        template.SetName(request.Name);
        template.SetExercises(exercises);
        await _tRepo.Update(template);
        return NoContent();
    }
}