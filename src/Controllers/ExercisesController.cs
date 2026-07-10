using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("/exercises")]
public class ExercisesController : ControllerBase
{
    private readonly IExercisesRepository _eRepo;
    private readonly IMuscleGroupsRepository _mRepo;
    public ExercisesController(IExercisesRepository eRepo, IMuscleGroupsRepository mRepo)
        => (_eRepo, _mRepo) = (eRepo, mRepo);

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var exercises = await _eRepo.GetAll();
        if (exercises == null) return NotFound();

        return Ok(exercises.Select(e => e.ToDTO()));
    }

    [HttpGet("get-muscle-groups")]
    public async Task<IActionResult> GetAllMuscleGroups()
    {
        var muscles = await _mRepo.GetAll();
        if (muscles == null) return NotFound();

        return Ok(muscles.Select(m => m.ToDTO()));
    }

    [HttpGet("get-filtered")]
    public async Task<IActionResult> GetFiltered([FromBody] ExerciseSearchFilterDTO filter)
    {
        var exercises = await _eRepo.GetFiltered(filter.Name, filter.MuscleGroups);
        if (exercises == null) return NotFound();

        return Ok(exercises.Select(e => e.ToDTO()));
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var exercise = await _eRepo.Get(id);
        if (exercise == null) return NotFound();

        return Ok(exercise.ToDTO());
    }
}