using System.Collections.Generic;

public class ExerciseEntity : IEntityBase
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<MuscleGroupEntity> Muscles { get; private set; } = [];
    public ICollection<TemplateEntity> Templates { get; private set; } = [];
    public ICollection<SessionExerciseEntity> SessionExercises { get; private set; } = [];

    private ExerciseEntity() {}
    public ExerciseEntity(string name, IEnumerable<MuscleGroupEntity> muscles)
        => (Name, Muscles) = (name, muscles.ToArray());
}