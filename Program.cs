using AcademyApi.Data;
using AcademyApi.Repositories;
using Npgsql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This is for my Repository classes
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();

builder.Services.AddDbContext<DataContext>(optionsBuilder =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DataContext");
        optionsBuilder
            .UseNpgsql(connectionString).LogTo(Console.WriteLine);
    },
    ServiceLifetime.Scoped,
    ServiceLifetime.Singleton
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
        throw new Exception("Database is not fully migrated for Movies Context");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
