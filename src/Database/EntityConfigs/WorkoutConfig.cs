using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class WorkoutConfig : IEntityTypeConfiguration<WorkoutEntity>
{
    public void Configure(EntityTypeBuilder<WorkoutEntity> builder)
    {
        builder.ToTable("Workouts");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
                .ValueGeneratedOnAdd();

        builder.HasOne(w => w.User)
                .WithMany(u => u.Workouts)
                .HasForeignKey(w => w.UserId)
                .IsRequired(true);

        builder.Property(w => w.StartTime)
                .IsRequired(true);

        builder.Property(w => w.EndTime)
                .IsRequired(true);
    }
}