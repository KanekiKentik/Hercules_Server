using Microsoft.EntityFrameworkCore;

public class HerculesContext : DbContext
{
    public DbSet<UserEntity> Users { get; private set; }
    public DbSet<WorkoutEntity> Workouts { get; private set; }
    public DbSet<ExerciseEntity> Exercises { get; private set; }
    public DbSet<SessionExerciseEntity> SessionExercises { get; private set; }
    public DbSet<MuscleGroupEntity> MuscleGroups { get; private set; }
    public DbSet<SetEntity> Sets { get; private set; }
    public DbSet<TemplateEntity> Templates { get; private set; }
    public DbSet<UselessEntity> Useless { get; private set; }

    public HerculesContext(DbContextOptions<HerculesContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ExerciseConfig());
        modelBuilder.ApplyConfiguration(new MuscleGroupConfig());
        modelBuilder.ApplyConfiguration(new SessionExerciseConfig());
        modelBuilder.ApplyConfiguration(new SetConfig());
        modelBuilder.ApplyConfiguration(new TemplateConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new WorkoutConfig());

        base.OnModelCreating(modelBuilder);
    }
}

public class UselessEntity()
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
}