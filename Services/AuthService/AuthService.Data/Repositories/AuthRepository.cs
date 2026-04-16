using AuthService.Data.Entities;
using AuthService.Data.Interfaces;

namespace AuthService.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AuthDbContext _context;

    public AuthRepository(AuthDbContext context)
    {
        _context = context;
    }

    public User? GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public User CreateUser(string username, string passwordHash)
    {
        var user = new User
        {
            Username = username,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public bool UserExists(string username)
    {
        return _context.Users.Any(u => u.Username == username);
    }
}