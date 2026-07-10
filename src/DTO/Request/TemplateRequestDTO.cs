using System.ComponentModel.DataAnnotations;

public record TemplateRequestDTO
{
    [Required]
    [MaxLength(ValidationConstants.MAX_TEMPLATE_NAME_LENGTH)]
    public string Name { get; init; } = string.Empty;

    [MinLength(1)]
    public int[] ExerciseIds { get; init; } = [];

    public TemplateRequestDTO() {}
    public TemplateRequestDTO(string name, int[] ids)
        => (Name, ExerciseIds) = (name, ids.ToArray());
}