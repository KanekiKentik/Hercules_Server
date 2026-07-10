public static class WorkoutMapper
{
    public static WorkoutSummaryDTO ToSummaryDTO(this WorkoutEntity workout)
    {
        return new (workout.Id, workout.StartTime, workout.EndTime);
    }

    public static WorkoutDetailedDTO ToDetailedDTO(this WorkoutEntity workout)
    {
        var sessionExercises = workout.SessionExercises;
        if (sessionExercises is not { Count: > 0 })
            throw new InvalidOperationException($"Session Exercises are not included for workout with id: {workout.Id}");

        return new (workout.Id, workout.StartTime, workout.EndTime, sessionExercises.Select(s => s.ToDTO()));
    }

    public static WorkoutEntity ToEntity(this WorkoutCreateDTO wInfo, int userId)
    {
        return new (userId, wInfo.StartDateTime, wInfo.EndDateTime, wInfo.SessionExercises.Select(s => s.ToEntity()));
    }
}