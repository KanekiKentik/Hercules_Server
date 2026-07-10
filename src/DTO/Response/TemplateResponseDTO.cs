using System.ComponentModel.DataAnnotations;

public record TemplateResponseDTO
{
    public int TemplateId { get; init; }

    [Required]
    [MaxLength(ValidationConstants.MAX_TEMPLATE_NAME_LENGTH)]
    public string Name { get; init; } = string.Empty;

    [MinLength(1)]
    public int[] ExerciseIds { get; init; } = [];

    public TemplateResponseDTO(int templateId, string name, int[] ids)
        => (TemplateId, Name, ExerciseIds) = (templateId, name, ids.ToArray());
}