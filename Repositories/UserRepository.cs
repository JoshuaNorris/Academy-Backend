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
    Task<User?> AttemptLogin(string email, string hashedPassword);
    Task<bool> DeleteUser(string email);
    Task<bool> ChangePassword(string email, string oldPassword, string newPassword);
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

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(m => m.Email == email);
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

    public async Task<User?> AttemptLogin(string email, string password)
    {
        User? user = await GetByEmail(email);
        bool verify = VerifyPassword(password, user.HashedPassword);
        if (user != null && verify)
        {
            return user;
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

    public async Task<bool> ChangePassword(string email, string oldPassword, string newPassword)
    {
        User user = await AttemptLogin(email, oldPassword);
        if (user == null)
            return false;

        user.HashedPassword = HashPassword(newPassword);

        await _context.SaveChangesAsync();
       
        return true;
    }
}