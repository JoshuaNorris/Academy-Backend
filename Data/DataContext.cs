using AcademyApi.Models;
using AcademyApi.Data.EntityMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using System.Diagnostics.Eventing.Reader;

namespace AcademyApi.Data;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<UserToBook> UserToBooks => Set<UserToBook>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=academy;Username=postgres;Password=jmnj;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new BookMapping());
        modelBuilder.ApplyConfiguration(new UserToBookMapping());
        modelBuilder.ApplyConfiguration(new AuthorMapping());
    }

}