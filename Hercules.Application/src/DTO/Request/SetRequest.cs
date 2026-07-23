using System.ComponentModel.DataAnnotations;

public record SetRequest
{
    [Range(1, int.MaxValue)]
    public int SessionExerciseId { get; init; }

    [Range(0, 600)]
    public int Weight { get; init; }

    [Range(1, int.MaxValue)]
    public int Reps { get; init; }

    public SetRequest() {}
    public SetRequest(int sessionId, int weight, int reps)
        => (SessionExerciseId, Weight, Reps) = (sessionId, weight, reps);
}