using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExerciseConfig : IEntityTypeConfiguration<ExerciseEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.ToTable("Exercises");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

        builder.HasMany(e => e.SessionExercises)
                .WithOne(s => s.Exercise)
                .HasForeignKey(s => s.ExerciseId)
                .IsRequired(true);
    }
}