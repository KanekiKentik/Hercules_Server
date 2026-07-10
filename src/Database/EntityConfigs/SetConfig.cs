using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SetConfig : IEntityTypeConfiguration<SetEntity>
{
    public void Configure(EntityTypeBuilder<SetEntity> builder)
    {
        builder.ToTable("Sets");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();

        builder.HasOne(s => s.SessionExercise)
                .WithMany(e => e.Sets)
                .HasForeignKey(s => s.SessionExerciseId)
                .IsRequired(true);
    }
}