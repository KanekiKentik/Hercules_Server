public class SetEntity : IEntityBase
{
    public int Id { get; private set; }
    public int SessionExerciseId { get; private set; }
    public SessionExerciseEntity SessionExercise { get; private set; } = null!;
    public int Weight { get; private set; }
    public int Reps { get; private set; }
    public int Order { get; private set; }

    private SetEntity() {}
    internal SetEntity(int weight, int reps, int order)
        => (Weight, Reps, Order) = (weight, reps, order);

    internal void SetWeight(int weight) => Weight = weight;
    internal void SetReps(int reps) => Reps = reps;
}