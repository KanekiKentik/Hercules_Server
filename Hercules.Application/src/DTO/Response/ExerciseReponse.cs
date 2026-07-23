using System.ComponentModel.DataAnnotations;

public record ExerciseResponse
{
    [Range(0, int.MaxValue)]
    public int ExerciseId { get; init; }

    [Required]
    [MaxLength(ValidationConstants.MAX_EXERCISE_NAME_LENGTH)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [MinLength(1)]
    public MuscleGroupResponse[] MuscleGroups { get; init; } = [];
    public ExerciseResponse(int id, string name, IEnumerable<MuscleGroupResponse> muscles)
        => (ExerciseId, Name, MuscleGroups) = (id, name, muscles.ToArray());
}