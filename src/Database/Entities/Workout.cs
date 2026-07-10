public class WorkoutEntity
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public UserEntity User { get; private set; } = null!;
    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset EndTime { get; private set; }
    public ICollection<SessionExerciseEntity> SessionExercises { get; private set; } = [];

    private WorkoutEntity() {}
    public WorkoutEntity(int userId, DateTimeOffset start, DateTimeOffset end, IEnumerable<SessionExerciseEntity> sessionExercises)
        => (UserId, StartTime, EndTime, SessionExercises) = (userId, start, end, sessionExercises.ToArray());
}