using System.ComponentModel.DataAnnotations;

public record WorkoutDetailedDTO
{
    [Range(0, int.MaxValue)]
    public int WorkoutId { get; init; }
    public DateTimeOffset StartDateTime { get; init; } = default;
    public DateTimeOffset EndDateTime { get; init; } = default;

    [MinLength(1)]
    public SessionExerciseResponseDTO[] SessionExercises { get; init; } = [];

    public WorkoutDetailedDTO(int id, DateTimeOffset start, DateTimeOffset end, IEnumerable<SessionExerciseResponseDTO> sessionExercises)
        => (WorkoutId, StartDateTime, EndDateTime, SessionExercises) = (id, start, end, sessionExercises.ToArray());
}