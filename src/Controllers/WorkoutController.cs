using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("/workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutsRepository _wRepo;
    private readonly UsersService _uService;
    private readonly ISessionExercisesRepository _seRepo;
    private readonly ISetsRepository _sRepo;
    public WorkoutsController(IWorkoutsRepository wRepo, UsersService uService, ISessionExercisesRepository seRepo, ISetsRepository sRepo)
        => (_wRepo, _uService, _seRepo, _sRepo) = (wRepo, uService, seRepo, sRepo);

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        int userId = _uService.GetSelfId(User);

        WorkoutEntity[] userWorkouts = await _wRepo.GetAll(userId);
        return Ok(userWorkouts.Select(w => w.ToSummaryDTO()));
    }

    [HttpGet("get-all-filtered")]
    public async Task<IActionResult> GetAllFiltered([FromBody] WorkoutSearchFilterDTO filter)
    {
        int userId = _uService.GetSelfId(User);

        WorkoutEntity[] userWorkouts = await _wRepo.GetAllFiltered(userId, filter.DateFrom, filter.DateTo);
        return Ok(userWorkouts.Select(w => w.ToSummaryDTO()));
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var workout = await _wRepo.Get(id);
        if (workout == null) return NotFound();

        int userId = _uService.GetSelfId(User);
        if (workout.UserId != userId) return Forbid();

        return Ok(workout.ToDetailedDTO());

    }

    [HttpPost("post")]
    public async Task<IActionResult> Post([FromBody] WorkoutCreateDTO wInfo)
    {
        int userId = _uService.GetSelfId(User);

        WorkoutEntity workout = wInfo.ToEntity(userId);
        await _wRepo.Post(workout);

        return Created();
    }

    [HttpDelete("session-exercises/delete")]
    public async Task<IActionResult> DeleteSessionExercise([FromQuery] int id)
    {
        int? workoutId = await _seRepo.GetWorkoutId(id);

        if (!workoutId.HasValue) return NotFound();

        var workout = await _wRepo.Get(workoutId.Value);
        if (workout == null) throw new Exception("Session exercise with no binded workout");

        int userId = _uService.GetSelfId(User);
        if (userId != workout.UserId) return Forbid();

        await _seRepo.Delete(id);
        return NoContent();
    }

    [HttpDelete("sets/delete")]
    public async Task<IActionResult> DeleteSet([FromQuery] int id)
    {
        int? workoutId = await _sRepo.GetWorkoutId(id);

        if (!workoutId.HasValue) return NotFound();

        var workout = await _wRepo.Get(workoutId.Value);
        if (workout == null) throw new Exception("Set with no binded workout");

        int userId = _uService.GetSelfId(User);
        if (userId != workout.UserId) return Forbid();

        await _sRepo.Delete(id);
        return NoContent();
    }

    [HttpPatch("sets/update")]
    public async Task<IActionResult> UpdateSet([FromQuery] int id, [FromQuery] int weight, [FromQuery] int reps)
    {
        var set = await _sRepo.Get(id);
        if (set == null) return NotFound();

        int userId = _uService.GetSelfId(User);
        int? workoutId = await _sRepo.GetWorkoutId(id);
        if (!workoutId.HasValue) 
            throw new UnattachedDataException("Set with no workout");

        var workout = await _wRepo.Get(workoutId.Value);
        if (workout!.UserId != userId) return Forbid();

        set.SetWeight(weight);
        set.SetReps(reps);

        await _sRepo.Update(set);
        return NoContent();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        int userId = _uService.GetSelfId(User);

        var workout = await _wRepo.Get(id);
        if (workout == null) return NotFound();

        if (workout.UserId != userId) return Forbid();

        await _wRepo.Delete(id);
        return NoContent();
    }
}