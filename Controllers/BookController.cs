using AcademyApi.Data;
using AcademyApi.Models;
using AcademyApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.OpenApi.Validations;


namespace AcademyApi.Controllers;


[Route("Books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;

    public BookController(IBookRepository repository, IAuthorRepository authorRepository)
    {
        _repository = repository;
        _authorRepository = authorRepository;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBooks()
    {
        return Ok(await _repository.GetAllBooks());
    }

    [HttpGet("{title}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTitle([FromRoute] string title)
    {
        var book = await _repository.GetByTitle(title);

        return book == null ? NotFound() : Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewBook([FromBody] Book book)
    {
        var newAuthor = new Author
        {
            FirstName = book.Author.FirstName,
            LastName = book.Author.LastName,
        };
        await _authorRepository.CreateNewAuthor(newAuthor, book);
        int authorId = newAuthor.Id;
        book.AuthorId = authorId;
        book.Author = newAuthor;
        await _repository.CreateNewBook(book);

        return  CreatedAtAction(nameof(GetByTitle), new { title = book.Title }, book);
    }
    /*
    [HttpDelete("{title}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveBook([FromRoute] string title)
    {
        var existingBook = await _context.Books.SingleOrDefaultAsync(m => m.Title == title);

        if (existingBook is null)
            return NotFound();

        _context.Books.Remove(existingBook);

        await _context.SaveChangesAsync();

        return Ok();
    }
    */
}