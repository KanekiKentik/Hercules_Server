using System.ComponentModel.DataAnnotations;

public record SessionExerciseResponseDTO
{
    [Range(0, int.MaxValue)]
    public int SessionExerciseId { get; init; }

    [Range(0, int.MaxValue)]
    public int ExerciseId { get; init; }

    [Range(0, int.MaxValue)]
    public int Order { get; init; }

    [MinLength(1)]
    public SetResponseDTO[] Sets { get; init; } = [];

    public SessionExerciseResponseDTO(int id, int exerciseId, int order, IEnumerable<SetResponseDTO> sets)
        => (SessionExerciseId, ExerciseId, Order, Sets) = (id, exerciseId, order, sets.ToArray());
}