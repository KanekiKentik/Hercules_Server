using System.ComponentModel.DataAnnotations;

public record WorkoutDetailedResponse
{
    [Range(0, int.MaxValue)]
    public int WorkoutId { get; init; }
    public DateTimeOffset StartDateTime { get; init; } = default;
    public DateTimeOffset? EndDateTime { get; init; } = default;

    [MinLength(1)]
    public SessionExerciseResponse[] SessionExercises { get; init; } = [];

    public WorkoutDetailedResponse(int id, DateTimeOffset start, DateTimeOffset? end, IEnumerable<SessionExerciseResponse> sessionExercises)
        => (WorkoutId, StartDateTime, EndDateTime, SessionExercises) = (id, start, end, sessionExercises.ToArray());
}