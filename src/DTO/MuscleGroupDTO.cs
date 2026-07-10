using System.ComponentModel.DataAnnotations;

public record MuscleGroupDTO
{
    public int MuscleGroupId { get; init; }

    [Required]
    [MaxLength(ValidationConstants.MAX_MUSCLE_NAME_LENGTH)]
    public string Name {get; init; } = string.Empty;

    public MuscleGroupDTO(int id, string name)
        => (MuscleGroupId, Name) = (id, name);
}