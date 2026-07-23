public static class WorkoutMapper
{
    public static WorkoutSummaryResponse ToSummaryDTO(this WorkoutEntity workout)
    {
        return new (workout.Id, workout.StartTime, workout.EndTime);
    }

    public static Result<WorkoutDetailedResponse> ToDetailedDTO(this WorkoutEntity workout)
    {
        if (workout.SessionExercises == null)
            return Result<WorkoutDetailedResponse>.Failure(
                ErrorType.InvalidOperation,
                $"Session Exercises are not included for workout with id: {workout.Id}");

        var sessionResponses = new List<SessionExerciseResponse>();
        foreach (var s in workout.SessionExercises)
        {
            var result = s.ToResponse();

            if (!result.IsSuccess)
                return Result<WorkoutDetailedResponse>.Failure(result.ErrorType, result.Message);
            sessionResponses.Add(result.Value);
        }

        var response = new WorkoutDetailedResponse(workout.Id, workout.StartTime, workout.EndTime, sessionResponses.ToArray());
        return Result<WorkoutDetailedResponse>.Success(response);
    }
}