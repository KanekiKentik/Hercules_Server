using System.ComponentModel.DataAnnotations;

public record MuscleGroupResponse
{
    public int MuscleGroupId { get; init; }

    [Required]
    [MaxLength(ValidationConstants.MAX_MUSCLE_NAME_LENGTH)]
    public string Name {get; init; } = string.Empty;

    public MuscleGroupResponse(int id, string name)
        => (MuscleGroupId, Name) = (id, name);
}