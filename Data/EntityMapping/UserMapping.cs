using AcademyApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyApi.Data.EntityMapping;

public class UserMapping: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(user => user.Email);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(255);

        builder.Property(user => user.DisplayName)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(30);

        builder.Property(user => user.FirstName)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(30);

        builder.Property(user => user.LastName)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(30);

        builder.Property(user => user.HashedPassword)
            .IsRequired()
            .HasColumnType("char")
            .HasMaxLength(60);

        builder.Property(user => user.UserRole)
            .IsRequired()
            .HasColumnType("smallint");
    }
}
