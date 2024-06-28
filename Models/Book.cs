using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Serialization;

namespace AcademyApi.Models;

// This warning I am disabled is about how I am not specifying that it is required
// But I am making the fields required in the DataContext.
#pragma warning disable CS8618
public class Book
{

    public int Id { get; set; }
    public string Title { get; set; }
    public Author Author { get; set; }
    public int AuthorId { get; set; }

    public string Description { get; set; }
    // public byte[] BookCover { get; set; }

    [JsonIgnore]
    public ICollection<UserToBook> UserToBooks { get; set; } = new HashSet<UserToBook>();

}