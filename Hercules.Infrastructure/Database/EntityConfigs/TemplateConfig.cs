using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TemplateConfig : IEntityTypeConfiguration<TemplateEntity>
{
    public void Configure(EntityTypeBuilder<TemplateEntity> builder)
    {
        builder.ToTable("Templates");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

        builder.HasMany(t => t.Exercises)
                .WithMany(e => e.Templates)
                .UsingEntity(j => j.ToTable("TemplatesExercises"));

        builder.HasOne(t => t.User)
                .WithMany(u => u.Templates)
                .HasForeignKey(t => t.UserId)
                .IsRequired(true);

        builder.Property(t => t.Name)
                .IsRequired(true)
                .HasMaxLength(ValidationConstants.MAX_TEMPLATE_NAME_LENGTH);
    }
}