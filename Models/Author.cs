using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Serialization;

namespace AcademyApi.Models;

// This warning I am disabled is about how I am not specifying that it is required
// But I am making the fields required in the DataContext.
#pragma warning disable CS8618
public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    // The JSON Ignore is here because I should be accessing this through books.
    [JsonIgnore]
    public ICollection<Book> Books { get; set; } = new HashSet<Book>();
}