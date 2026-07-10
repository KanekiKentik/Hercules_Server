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
        int userId = _uService.GetSelfId(User);
        var templates = await _tRepo.GetAll(userId);

        return Ok(templates.Select(t => t.ToResponseDTO()));
    }

    [HttpPost("post")]
    public async Task<IActionResult> Post([FromBody] TemplateRequestDTO request)
    {
        //Если кидает в запрос айди упражнений, которые не существуют => игнорирует их
        var exercises = await _eRepo.GetAll(request.ExerciseIds, true);
        if (exercises is not { Length: > 0 }) return NotFound();

        int userId = _uService.GetSelfId(User);

        TemplateEntity template = new (userId, request.Name, exercises);
        await _tRepo.Post(template);
        return Created();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        int userId = _uService.GetSelfId(User);

        var template = await _tRepo.Get(id);
        if (template == null) return NotFound();
        if (template.UserId != userId) return Forbid();

        await _tRepo.Delete(id);
        return NoContent();
    }

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromQuery] int id, [FromBody] TemplateRequestDTO request)
    {
        int userId = _uService.GetSelfId(User);

        var template = await _tRepo.Get(id, true);
        if (template == null) return NotFound("Template not found");
        if (template.UserId != userId) return Forbid();

        var exercises = await _eRepo.GetAll(request.ExerciseIds, true);
        if (exercises is not { Length: > 0 }) return NotFound("Template exercises not found");

        template.SetName(request.Name);
        template.SetExercises(exercises);
        await _tRepo.Update(template);
        return NoContent();
    }
}