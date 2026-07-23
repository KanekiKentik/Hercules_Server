using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ExerciseConfig : IEntityTypeConfiguration<ExerciseEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.ToTable("Exercises");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Name)
            .IsUnique(true);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsUnicode(true)
            .IsRequired(true)
            .HasMaxLength(ValidationConstants.MAX_EXERCISE_NAME_LENGTH);

        builder.HasMany(e => e.SessionExercises)
            .WithOne(s => s.Exercise)
            .HasForeignKey(s => s.ExerciseId)
            .IsRequired(true);
    }
}