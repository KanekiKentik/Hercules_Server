public class MuscleGroupEntity : IEntityBase
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<ExerciseEntity> Exercises { get; private set; } = [];
    
    private MuscleGroupEntity() {}
    public MuscleGroupEntity(string name) => Name = name;
}