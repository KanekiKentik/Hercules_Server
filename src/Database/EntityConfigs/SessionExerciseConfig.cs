using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SessionExerciseConfig : IEntityTypeConfiguration<SessionExerciseEntity>
{
    public void Configure(EntityTypeBuilder<SessionExerciseEntity> builder)
    {
        builder.ToTable("SessionExercises");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();

        builder.HasOne(s => s.Workout)
                .WithMany(w => w.SessionExercises)
                .HasForeignKey(s => s.WorkoutId)
                .IsRequired(true);
    }
}