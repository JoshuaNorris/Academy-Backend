using AcademyApi.Models;
using AcademyApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.OpenApi.Validations;
using System.Linq;
using BCrypt.Net;

namespace AcademyApi.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllStudents();
    Task<User?> GetByEmail(string email);
    Task CreateUser(User user);
    string HashPassword(string password);
    bool VerifyPassword(string givenPassword, string hashedPassword);
    Task<LoginResponse?> AttemptLogin(string email, string hashedPassword);
    Task<bool> DeleteUser(string email);
    // Task<bool> ChangePassword(string email, string oldPassword, string newPassword);
}

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllStudents()
    {
        return await _context.Users
            .Where(user => user.UserRole >= UserRole.Student)
            .ToListAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        try
        {
            // Use SingleOrDefaultAsync to retrieve a user by email
            User user = await _context.Users
                .SingleOrDefaultAsync(user => user.Email == email);

            return user;
        }
        catch (Exception ex)
        {
            // Handle any exceptions according to your application's needs
            Console.WriteLine($"Error fetching user by email: {ex.Message}");
            throw; // or handle the exception gracefully
        }
    }

    public async Task CreateUser(User user)
    {
        user.HashedPassword = HashPassword(user.HashedPassword);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string givenPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(givenPassword, hashedPassword);
    }

    public async Task<LoginResponse?> AttemptLogin(string email, string password)
    {
        // Ensure GetByEmail(email) returns a valid User or null
        User? user = await GetByEmail(email);
        if (user == null)
        {    
            return null;
        }
        
        // Check if user.HashedPassword is not null before using it
        if (user.HashedPassword == null)
        {
            return null;
        }

        // Verify the password
        bool verify = VerifyPassword(password, user.HashedPassword);
        if (verify)
        {
            LoginResponse response = new LoginResponse
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserRole = user.UserRole
            };
            return response;
        }

        return null;
    }

    public async Task<bool> DeleteUser(string email)
    {
        var existingUser = await _context.Users.SingleOrDefaultAsync(m => m.Email == email);

        if (existingUser is null)
            return false;

        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();

        return true;
    }

    // public async Task<bool> ChangePassword(string email, string oldPassword, string newPassword)
    // {
    //     User user = await AttemptLogin(email, oldPassword);
    //     if (user == null)
    //         return false;

    //     user.HashedPassword = HashPassword(newPassword);

    //     await _context.SaveChangesAsync();
       
    //     return true;
    // }
}