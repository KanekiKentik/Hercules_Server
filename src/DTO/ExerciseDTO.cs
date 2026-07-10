using System.ComponentModel.DataAnnotations;

public record ExerciseDTO
{
    [Range(0, int.MaxValue)]
    public int ExerciseId { get; init; }

    [Required]
    [MaxLength(ValidationConstants.MAX_EXERCISE_NAME_LENGTH)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [MinLength(1)]
    public MuscleGroupDTO[] MuscleGroups { get; init; } = [];
    public ExerciseDTO(int id, string name, IEnumerable<MuscleGroupDTO> muscles)
        => (ExerciseId, Name, MuscleGroups) = (id, name, muscles.ToArray());
}