using AcademyApi.Data;
using AcademyApi.Models;
using AcademyApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.OpenApi.Validations;


namespace AcademyApi.Controllers;

// This will get removed, but just for testing right now.
[Route("Users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("students")]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAllStudents()
    {
        var students = await _repository.GetAllStudents();

        if (students == null || students.Count() == 0)
        {
            return NotFound();
        }
        else
        {
            return Ok(students);
        }
    }

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromRoute] string email)
    {
        var user = await _repository.GetByEmail(email);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewUser([FromBody] User user)
    {
        await _repository.CreateUser(user);

        return CreatedAtAction(nameof(GetByEmail), new { email = user.Email }, user);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AttemptLogin([FromBody] LoginRequest loginRequest)
    {
        LoginResponse? user = await _repository.AttemptLogin(loginRequest.Email, loginRequest.Password);
        return user == null ? NotFound() : Ok(user);
    }


    [HttpPut("changePassword")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    // {
    //     bool changeAttempt = await _repository.ChangePassword(request.Email, request.OldPassword, request.NewUnhashedPassword);
    //     if (changeAttempt)
    //         return Ok();
    //     else return NotFound();
    // }


    /*
        [HttpPut("{email}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeDisplayName([FromRoute] string email, [FromBody] User user)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(m => m.Email == email);
            // When we do this, existingUser is actually a proxy class that inherits from the Users model.
            // Because it is a proxy it actually keeps a connection to the DBContext
            // So when we change the proxy, it notifies the DbContext that there anre changes that need to be persisted.

            if (existingUser is null)
                return NotFound();

            existingUser.DisplayName = user.DisplayName;

            await _context.SaveChangesAsync();
            // This updates the changes that have been tracked above into the database

            return Ok(existingUser);
        }

        */

    [HttpDelete("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveUser([FromRoute] string email)
    {
        bool removed = await _repository.DeleteUser(email);
        return removed == false ? NotFound() : Ok();
    }
}


public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class ChangePasswordRequest
{
    public string? Email { set; get; }
    public string? OldPassword { set; get; }
    public string? NewUnhashedPassword { set; get; }
}

// IQueryable<Movie> allMovies = _context.Movies;

// var filteredMovies = allMovies.Where(m =>  m.ReleaseDate.Year == year);
// Because this is also an IQueryable it is executed lazily... so it needs
// to be returned with an .ToListAsync() to execute.

// THe below is another way to perform the above linq statement.
// The above is using lambdas, while below matches what you would see in a SQL statement.
// The ordering for the sql statement is actually different than normal sql with the select at the end.
/*
var filtered movies =
    from movie in _context.Movies
    where movie.ReleaseDate.Year == year
    select movie;
*/