using System.ComponentModel.DataAnnotations;

public record SessionExerciseRequestDTO
{
    [Range(0, int.MaxValue)]
    public int ExerciseId { get; init; }

    [Range(0, int.MaxValue)]
    public int Order { get; init; }

    [MinLength(1)]
    public SetRequestDTO[] Sets { get; init; } = [];

    public SessionExerciseRequestDTO() {}
    public SessionExerciseRequestDTO(int workoutId, int exerciseId, int order, IEnumerable<SetRequestDTO> sets)
        => (ExerciseId, Order, Sets) = (exerciseId, order, sets.ToArray());
}