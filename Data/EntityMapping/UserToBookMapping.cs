using AcademyApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyApi.Data.EntityMapping;

public class UserToBookMapping : IEntityTypeConfiguration<UserToBook>
{
    public void Configure(EntityTypeBuilder<UserToBook> builder)
    {
        /*
         * Shadow property for the created at property, will have to make
         * The CReatedDateGenerator before making it.
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenrator<CreatedDateGenerator>();
        */

        builder
            .HasOne(utb => utb.User)
            .WithMany(u => u.UserToBooks);

        builder
            .HasOne(utb => utb.Book)
            .WithMany(b => b.UserToBooks);

        // Seed - data that needs to be created 
    }
}
