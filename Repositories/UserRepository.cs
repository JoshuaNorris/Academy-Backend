using AcademyApi.Models;
using AcademyApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AcademyApi.Repositories;

public interface IUserRepository
{
	Task CreateUser(User user);
	Task<IEnumerable<User>> GetUsers();
	Task<User?> GetByEmail(string email);
	Task<LoginResponse?> AttemptLogin(string email, string unhashedPassword);
}

public class UserRepository : IUserRepository
{
	private readonly DataContext _context;

	public UserRepository(DataContext context)
	{
		_context = context;
	}

	public async Task CreateUser(User user)
	{
		user.Password = HashPassword(user.Password);
		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
	}

	public string HashPassword(string password)
	{
		return BCrypt.Net.BCrypt.HashPassword(password);
	}

	public async Task<IEnumerable<User>> GetUsers()
	{
		return await _context.Users
            .ToListAsync();
	}

	public async Task<User?> GetByEmail(string email)
	{
		try
		{
			User? user = await _context.Users
				.SingleOrDefaultAsync(u => u.Email == email);
			return user;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error fetching user by email: {ex.Message}");
			throw;
		}
	}

public async Task<LoginResponse?> AttemptLogin(string email, string password)
    {
        // Ensure GetByEmail(email) returns a valid User or null
        User? user = await GetByEmail(email);
        if (user == null) {   return null;   }

        // Verify the password
        bool verify = VerifyPassword(password, user.Password);
        if (verify)
        {
            return new LoginResponse
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
        return null;
    }

	public bool VerifyPassword(string givenPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(givenPassword, hashedPassword);
    }
}