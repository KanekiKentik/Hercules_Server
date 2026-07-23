using System.ComponentModel.DataAnnotations;

public record SessionExerciseResponse
{
    [Range(0, int.MaxValue)]
    public int SessionExerciseId { get; init; }

    [Range(0, int.MaxValue)]
    public int ExerciseId { get; init; }

    [Range(0, int.MaxValue)]
    public int Order { get; init; }

    [MinLength(1)]
    public SetResponse[] Sets { get; init; } = [];

    public SessionExerciseResponse(int id, int exerciseId, int order, IEnumerable<SetResponse> sets)
        => (SessionExerciseId, ExerciseId, Order, Sets) = (id, exerciseId, order, sets.ToArray());
}