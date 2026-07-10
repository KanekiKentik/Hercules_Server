public static class ExerciseMapper
{
    public static ExerciseDTO ToDTO(this ExerciseEntity exercise)
    {
        return new (exercise.Id, exercise.Name, exercise.Muscles.Select(m => m.ToDTO()));
    }
}