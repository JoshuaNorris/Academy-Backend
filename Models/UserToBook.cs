using Microsoft.AspNetCore.SignalR;
using System.Text.Json.Serialization;

namespace AcademyApi.Models;

// This warning I am disabled is about how I am not specifying that it is required
// But I am making the fields required in the DataContext.
#pragma warning disable CS8618
public class UserToBook
{
    public int Id { get; set; }

    // The below annotation specifies the order that we want to access these entities so there
    // are no cycles
    // For this project I will want to access the USer -> UserToBook -> Book
    // But I don't have a use case right now that would be to see all the users that are on a book.
    

    [JsonIgnore]
    public User User { get; set; }
    public Book Book { get; set; }
}