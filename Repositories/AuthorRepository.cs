using AcademyApi.Models;
using AcademyApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.OpenApi.Validations;
using System.Linq;

namespace AcademyApi.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<Author?> GetByName(string firstName, string lastName);
    Task<bool> CheckAuthorExists(string firstName, string lastName);
    Task<bool> CreateNewAuthor(Author author, Book book);
    //Task<bool> RemoveAuthor(string firstName, string lastName);
}

public class AuthorRepository : IAuthorRepository
{
    private readonly DataContext _context;

    public AuthorRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author?> GetByName(string firstName, string lastName)
    {
        return await _context.Authors.SingleOrDefaultAsync(author => author.FirstName == firstName && author.LastName == lastName);
    }

    public async Task<bool> CheckAuthorExists(string firstName, string lastName)
    {
        return await _context.Authors.SingleOrDefaultAsync(author => author.FirstName == firstName && author.LastName == lastName) != null;
    }

    public async Task<bool> CreateNewAuthor(Author author, Book book)
    {
        
        author.Books.Add(book);
        if (await GetByName(author.FirstName, author.LastName) != null)
            return true;

        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return true;
    }
}