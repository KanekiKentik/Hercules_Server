using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("/workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutsRepository _wRepo;
    private readonly UsersService _uService;
    public WorkoutsController(IWorkoutsRepository wRepo, UsersService uService)
        => (_wRepo, _uService) = (wRepo, uService);

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        Result<int> getResult = _uService.GetSelfId(User);
        if (getResult.IsFailure) 
            return this.HandleErrorResult(getResult);

        int userId = getResult.Value;

        WorkoutEntity[] userWorkouts = await _wRepo.GetAll(userId, 100);
        return Ok(userWorkouts.Select(w => w.ToSummaryDTO()));
    }

    [HttpGet("get-all-filtered")]
    public async Task<IActionResult> GetAllFiltered([FromBody] WorkoutSearchFilter filter)
    {
        Result<int> getResult = _uService.GetSelfId(User);
        if (getResult.IsFailure) 
            return this.HandleErrorResult(getResult);

        int userId = getResult.Value;

        WorkoutEntity[] userWorkouts = await _wRepo.GetAllFiltered(userId, filter.DateFrom, filter.DateTo);
        return Ok(userWorkouts.Select(w => w.ToSummaryDTO()));
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] int workoutId)
    {
        var access = await CheckAccess(workoutId);
        if (access.IsFailure)
            return this.HandleErrorResult(access);

        var result = access.Value.ToDetailedDTO();
        if (result.IsFailure)
            return this.HandleErrorResult(result);
    
        return Ok(access.Value.ToDetailedDTO().Value);
    }

    [HttpPost("post")]
    public async Task<IActionResult> Post([FromQuery] DateTimeOffset? startTime)
    {
        if (!startTime.HasValue || startTime < DateTimeOffset.Parse("2000-01-01T00:00:00+00:00"))
            return BadRequest("Start time is invalid`");

        Result<int> getResult = _uService.GetSelfId(User);
        if (getResult.IsFailure) 
            return this.HandleErrorResult(getResult);

        int userId = getResult.Value;
        
        var workout = new WorkoutEntity(userId, startTime.Value);
        await _wRepo.Post(workout);

        return Created();
    }

    [HttpPatch("complete")]
    public async Task<IActionResult> Complete([FromQuery] int workoutId, [FromQuery] DateTimeOffset? endTime)
    {
        if (!endTime.HasValue || endTime < DateTimeOffset.Parse("2000-01-01T00:00:00+00:00"))
            return BadRequest("End time is invalid`");

        var result = await ModifyWorkout(workoutId, w => w.Complete(endTime.Value));
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int workoutId)
    {
        var access = await CheckAccess(workoutId);
        if (access.IsFailure)
            return this.HandleErrorResult(access);

        var deleteResult = await _wRepo.Delete(workoutId);
        if (deleteResult.IsFailure)
            return this.HandleErrorResult(deleteResult);

        return NoContent();
    }

    [HttpPatch("session-exercises/add")]
    public async Task<IActionResult> AddSessionExercise([FromQuery] int workoutId, [FromQuery] int exerciseId)
    {
        var result = await ModifyWorkout(workoutId, w => w.AddSessionExercise(exerciseId));
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    [HttpPatch("session-exercises/delete")]
    public async Task<IActionResult> RemoveSessionExercise([FromQuery] int workoutId, [FromQuery] int sessionId)
    {
        var result = await ModifyWorkout(workoutId, w => w.RemoveSessionExercise(sessionId));
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    [HttpPatch("sets/add")]
    public async Task<IActionResult> AddSet([FromQuery] int workoutId, [FromBody] SetRequest request)
    {
        var result = await ModifyWorkout(workoutId, w => w.AddSet(request.SessionExerciseId, request.Weight, request.Reps));
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    [HttpPatch("sets/update")]
    public async Task<IActionResult> UpdateSet([FromQuery] int workoutId, [FromQuery] int setId, [FromQuery] int weight, [FromQuery] int reps)
    {
        var result = await ModifyWorkout(workoutId, w => w.UpdateSet(setId, weight, reps));
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }
    [HttpPatch("sets/delete")]
    public async Task<IActionResult> DeleteSet([FromQuery] int workoutId, [FromQuery] int setId)
    {
        var result = await ModifyWorkout(workoutId, w => w.RemoveSet(setId));
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    private async Task<Result<WorkoutEntity>> CheckAccess(int workoutId)
    {
        var workout = await _wRepo.Get(workoutId, true);
        if (workout == null) return Result<WorkoutEntity>.Failure(ErrorType.NotFound);

        Result<int> getResult = _uService.GetSelfId(User);
        if (getResult.IsFailure) 
            return Result<WorkoutEntity>.Failure(getResult.ErrorType);

        int userId = getResult.Value;
        if (workout.UserId != userId)
            return Result<WorkoutEntity>.Failure(ErrorType.Forbidden);

        return Result<WorkoutEntity>.Success(workout);
    }
    private async Task<Result> ModifyWorkout(int workoutId, Func<WorkoutEntity, Result> action)
    {
        var access = await CheckAccess(workoutId);
        if (access.IsFailure)
            return Result.Failure(access.ErrorType);

        var workout = access.Value;

        var modify = action(workout);
        if (modify.IsFailure)
            return Result.Failure(modify.ErrorType);

        var update = await _wRepo.Update(workout);
        if (update.IsFailure)
            return Result.Failure(update.ErrorType);

        return Result.Success();
    }
}