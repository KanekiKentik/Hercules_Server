public static class ExerciseMapper
{
    public static Result<ExerciseResponse> ToResponse(this ExerciseEntity exercise)
    {
        var muscles = exercise.Muscles;
        if (muscles == null)
            return Result<ExerciseResponse>.Failure(ErrorType.InvalidOperation, $"Muscle Groups are not included for exercise with id: ${exercise.Id}");

        var response = new ExerciseResponse(exercise.Id, exercise.Name, exercise.Muscles.Select(m => m.ToResponse()));
        return Result<ExerciseResponse>.Success(response);
    }
}