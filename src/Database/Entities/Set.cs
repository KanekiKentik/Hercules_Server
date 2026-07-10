public class SetEntity
{
    public int Id { get; private set; }
    public int SessionExerciseId { get; private set; }
    public SessionExerciseEntity SessionExercise { get; private set; } = null!;
    public int Weight { get; private set; }
    public int Reps { get; private set; }
    public int Order { get; private set; }

    public SetEntity(int weight, int reps, int order)
        => (Weight, Reps, Order) = (weight, reps, order);

    public void SetWeight(int weight) => Weight = weight;
    public void SetReps(int reps) => Reps = reps;
}