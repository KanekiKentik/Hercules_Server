using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class UserConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Username)
                .IsUnique(true);

        builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

        builder.Property(u => u.Username)
                .HasMaxLength(ValidationConstants.MAX_USERNAME_LENGTH)
                .IsUnicode(true)
                .IsRequired(true);

        builder.Property(u => u.PasswordHash)
                .IsUnicode(true)
                .IsRequired(true);

        builder.Property(u => u.RegistrationDate)
                .IsRequired(true);

        builder.Property(u => u.Privilege)
                .HasConversion<string>();
    }
}