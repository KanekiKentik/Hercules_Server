public static class SessionExerciseMapper
{
    public static Result<SessionExerciseResponse> ToResponse(this SessionExerciseEntity entity)
    {
        var sets = entity.Sets;
        if (sets == null)
            return Result<SessionExerciseResponse>.Failure(
                    ErrorType.InvalidOperation,
                    $"Sets are not included for SessionExercise with id: {entity.Id}");

        var response = new SessionExerciseResponse(entity.Id, entity.ExerciseId, entity.Order, entity.Sets.Select(s => s.ToResponse()));
        return Result<SessionExerciseResponse>.Success(response);
    }
}