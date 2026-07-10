using System.Collections.Generic;

public class SessionExerciseEntity
{
    public int Id { get; private set; }
    public int WorkoutId { get; private set; }
    public WorkoutEntity Workout { get; private set;} = null!;
    public int ExerciseId { get; private set; }
    public ExerciseEntity Exercise { get; private set; } = null!;
    public ICollection<SetEntity> Sets { get; private set; } = [];
    public int Order { get; private set; }

    public SessionExerciseEntity() {}
    public SessionExerciseEntity( int exerciseId, int order, IEnumerable<SetEntity> sets)
        => (ExerciseId, Order, Sets) = (exerciseId, order, sets.ToArray());
}