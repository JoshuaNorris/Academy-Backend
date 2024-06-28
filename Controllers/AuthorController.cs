using AcademyApi.Data;
using AcademyApi.Models;
using AcademyApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.OpenApi.Validations;


namespace AcademyApi.Controllers;


[Route("Authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _repository;

    public AuthorController(IAuthorRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Author>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAuthors()
    {   
        return Ok(await _repository.GetAllAuthors());
    }
}