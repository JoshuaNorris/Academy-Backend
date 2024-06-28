using AcademyApi.Models;
using AcademyApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.OpenApi.Validations;
using System.Linq;

namespace AcademyApi.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book?> GetByTitle(string title);
    Task CreateNewBook(Book book);
    Task<bool> RemoveBook(string title);
    Task<bool> ChangeDescription(string title, string description);
    //Get description
}

public class BookRepository : IBookRepository
{
    private readonly DataContext _context;

    public BookRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await _context.Books
            .ToListAsync();
    }

    public async Task<Book?> GetByTitle(string title)
    {
        return await _context.Books.SingleOrDefaultAsync(book => book.Title == title);
    }

    public async Task CreateNewBook(Book book, Author author)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> RemoveBook(string title)
    {
        var existingBook = await _context.Books.SingleOrDefaultAsync(m => m.Title == title);
        if (existingBook is null)
            return false;
        _context.Books.Remove(existingBook);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ChangeDescription(string title, string description)
    {
        return default;
    }
}