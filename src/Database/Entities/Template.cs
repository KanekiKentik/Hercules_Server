using System.Collections.Generic;
public class TemplateEntity
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public UserEntity User { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public ICollection<ExerciseEntity> Exercises { get; private set; } = [];

    private TemplateEntity() {}
    public TemplateEntity(int userId, string name, IEnumerable<ExerciseEntity> exercises)
        => (UserId, Name, Exercises) = (userId, name, exercises.ToArray());

    public void SetName(string name) => Name = name;
    public void SetExercises(IEnumerable<ExerciseEntity> exercises)
    {
        Exercises.Clear();
        foreach(var exercise in exercises)
            Exercises.Add(exercise);
    }
}