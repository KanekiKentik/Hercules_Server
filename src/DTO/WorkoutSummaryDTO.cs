using System.ComponentModel.DataAnnotations;

public record WorkoutSummaryDTO
{
    [Range(0, int.MaxValue)]
    public int WorkoutId { get; init; }
    public DateTimeOffset StartDateTime { get; init; } = default;
    public DateTimeOffset EndDateTime { get; init; } = default;

    public WorkoutSummaryDTO(int workoutId, DateTimeOffset start, DateTimeOffset end)
        => (WorkoutId, StartDateTime, EndDateTime) = (workoutId, start, end);
}