using System.ComponentModel.DataAnnotations;

public record ExerciseSearchFilterDTO
{
    public string Name { get; init; } = string.Empty;
    public string[] MuscleGroups { get; init; } = [];
}