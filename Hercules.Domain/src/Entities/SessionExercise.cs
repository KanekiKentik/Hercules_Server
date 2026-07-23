public class SessionExerciseEntity : IEntityBase
{
    public int Id { get; private set; }
    public int WorkoutId { get; private set; }
    public WorkoutEntity Workout { get; private set;} = null!;
    public int ExerciseId { get; private set; }
    public ExerciseEntity Exercise { get; private set; } = null!;
    public ICollection<SetEntity> Sets { get; set; } = [];
    public int Order { get; private set; }

    private SessionExerciseEntity() {}
    internal SessionExerciseEntity(int workoutId, int exerciseId, int order)
        => (WorkoutId, ExerciseId, Order) = (workoutId, exerciseId, order);

    internal void AddSet(int weight, int reps)
    {
        int maxOrder = Sets.Count() > 0 ? Sets.Max(s => s.Order) : 0;
        var set = new SetEntity(weight, reps, ++maxOrder); 

        Sets.Add(set);
    }
    internal Result UpdateSet(int setId, int weight, int reps)
    {
        var set = Sets.FirstOrDefault(s => s.Id == setId);
        
        if (set == null) return Result.Failure(ErrorType.NotFound);

        set.SetWeight(weight);
        set.SetReps(reps);

        return Result.Success();
    }
    internal Result RemoveSet(int setId)
    {
        var set = Sets.FirstOrDefault(s => s.Id == setId);

        if (set == null) return Result.Failure(ErrorType.NotFound);

        Sets.Remove(set);
        return Result.Success();
    }
}