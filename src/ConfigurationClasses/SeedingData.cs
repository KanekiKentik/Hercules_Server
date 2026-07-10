public record SeedingData
{
    public string[] MuscleGroups { get; init; } = [];
    public ExerciseSeed[] Exercises { get; init; } = [];
}