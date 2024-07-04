using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Serialization;

namespace AcademyApi.Models;

// This warning I am disabled is about how I am not specifying that it is required
// But I am making the fields required in the DataContext.
#pragma warning disable CS8618
public class User
{
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string HashedPassword { get; set; }
    public UserRole UserRole { get; set; }

    public ICollection<UserToBook> UserToBooks { get; set; } = new HashSet<UserToBook>();

}

public enum UserRole
{
    Student,
    Administrator
}

public class LoginResponse
{
    
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole UserRole { get; set; }
}
