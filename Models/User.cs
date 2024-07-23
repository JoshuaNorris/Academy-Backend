using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Serialization;

namespace AcademyApi.Models;

#pragma warning disable CS8618
public class User
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; } // This is hashed
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class LoginResponse
{   
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}