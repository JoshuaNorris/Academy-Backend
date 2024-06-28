using AcademyApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyApi.Data.EntityMapping;

public class AuthorMapping : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {

        builder
            .HasKey(Author => Author.Id);

        builder.Property(author => author.FirstName)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(30);

        builder.Property(author => author.LastName)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(30);
    }
}
