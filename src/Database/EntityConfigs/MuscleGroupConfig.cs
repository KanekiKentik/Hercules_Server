using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MuscleGroupConfig : IEntityTypeConfiguration<MuscleGroupEntity>
{
    public void Configure(EntityTypeBuilder<MuscleGroupEntity> builder)
    {
        builder.ToTable("MuscleGroups");

        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.Name)
                .IsUnique(true);

        builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

        builder.Property(m => m.Name)
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(ValidationConstants.MAX_MUSCLE_NAME_LENGTH);

        builder.HasMany(m => m.Exercises)
                .WithMany(e => e.Muscles)
                .UsingEntity(j => j.ToTable("ExercisesMuscles"));
    }
}