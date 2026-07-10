using System.ComponentModel.DataAnnotations;

public record WorkoutCreateDTO
{
    public DateTimeOffset StartDateTime { get; init; } = default;
    public DateTimeOffset EndDateTime { get; init; } = default;

    [MinLength(1)]
    public SessionExerciseRequestDTO[] SessionExercises { get; init; } = [];

    public WorkoutCreateDTO() {}
    public WorkoutCreateDTO(int id, DateTimeOffset start, DateTimeOffset end, IEnumerable<SessionExerciseRequestDTO> sessionExercises)
        => (StartDateTime, EndDateTime, SessionExercises) = (start, end, sessionExercises.ToArray());
}