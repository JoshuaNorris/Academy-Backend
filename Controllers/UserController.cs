using Microsoft.AspNetCore.Mvc;
using AcademyApi.Models;
using AcademyApi.Repositories;
namespace AcademyApi.Controllers;

[Route("Users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUsers()
    {
        var students = await _repository.GetUsers();

        if (students == null || students.Count() == 0)
        {
            return NotFound();
        }
        else
        {
            return Ok(students);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewUser([FromBody] User user)
    {
        await _repository.CreateUser(user);

        return CreatedAtAction(nameof(GetByEmail), new { email = user.Email }, user);
    }

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromRoute] string email)
    {
        var user = await _repository.GetByEmail(email);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AttemptLogin([FromBody] LoginRequest loginRequest)
    {
        LoginResponse? user = await _repository.AttemptLogin(loginRequest.Email, loginRequest.Password);
        return user == null ? NotFound() : Ok(user);
    }
}

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}