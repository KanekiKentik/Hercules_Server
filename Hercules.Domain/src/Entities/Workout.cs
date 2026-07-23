public class WorkoutEntity : IEntityBase
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public UserEntity User { get; private set; } = null!;
    public DateTimeOffset StartTime { get; private set; } = default;
    public DateTimeOffset? EndTime { get; private set; } = null;
    public ICollection<SessionExerciseEntity> SessionExercises { get; set; } = [];
    public bool IsCompleted { get => EndTime.HasValue; }
    private WorkoutEntity() {}
    public WorkoutEntity(int userId, DateTimeOffset startTime)
        => (UserId, StartTime) = (userId, startTime);

    public Result Complete(DateTimeOffset endTime)
    {
        if (IsCompleted)
            return Result.Failure(ErrorType.InvalidOperation, "Workout is already completed");

        if (SessionExercises.Count == 0)
            return Result.Failure(ErrorType.InvalidOperation, "Cannot complete an empty workout");

        if (SessionExercises.Any(s => s.Sets.Count == 0))
            return Result.Failure(ErrorType.InvalidOperation, "Cannot complete workout with an empty session exercise");

        if (StartTime >= endTime)
            return Result.Failure(ErrorType.InvalidOperation, "Workout cannot be completed earlier than it started");

        EndTime = endTime;
        return Result.Success();
    }
    public Result AddSessionExercise(int exerciseId)
    {
        if (IsCompleted) 
            return Result.Failure(ErrorType.InvalidOperation, "Cannot change completed workout");

        int maxOrder = SessionExercises.Count() > 0 ? SessionExercises.Max(s => s.Order) : 0;
        var sessionExercise = new SessionExerciseEntity(Id, exerciseId, ++maxOrder);

        SessionExercises.Add(sessionExercise);
        return Result.Success();
    }
    public Result RemoveSessionExercise(int sessionExerciseId)
    {
        if (IsCompleted) 
            return Result.Failure(ErrorType.InvalidOperation, "Cannot change completed workout");

        var sessionExercise = SessionExercises.FirstOrDefault(s => s.Id == sessionExerciseId);
        if (sessionExercise == null) return Result.Failure(ErrorType.NotFound);

        SessionExercises.Remove(sessionExercise);
        return Result.Success();
    }
    public Result AddSet(int sessionExerciseId, int weight, int reps)
    {
        if (IsCompleted) 
            return Result.Failure(ErrorType.InvalidOperation, "Cannot change completed workout");

        var sessionExercise = SessionExercises.FirstOrDefault(s => s.Id == sessionExerciseId);
        if (sessionExercise == null) return Result.Failure(ErrorType.NotFound);

        sessionExercise.AddSet(weight, reps);
        return Result.Success();
        
    }
    public Result UpdateSet(int setId, int weight, int reps)
    {
        if (IsCompleted) 
            return Result.Failure(ErrorType.InvalidOperation, "Cannot change completed workout");

        var sessionExercise = SessionExercises.FirstOrDefault(s => s.Sets.Any(s => s.Id == setId));
        if (sessionExercise == null) return Result.Failure(ErrorType.NotFound);

        var result = sessionExercise.UpdateSet(setId, weight, reps);
        if (!result.IsSuccess) return result;

        return Result.Success();
    }
    public Result RemoveSet(int setId)
    {
        if (IsCompleted) 
            return Result.Failure(ErrorType.InvalidOperation, "Cannot change completed workout");

        var sessionExercise = SessionExercises.FirstOrDefault(s => s.Sets.Any(s => s.Id == setId));
        if (sessionExercise == null) return Result.Failure(ErrorType.NotFound);

        var result = sessionExercise.RemoveSet(setId);
        if (!result.IsSuccess) return result;

        return Result.Success();
    }
}