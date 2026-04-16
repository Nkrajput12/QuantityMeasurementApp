using AuthService.Data.Entities;

namespace AuthService.Data.Interfaces;

public interface IAuthRepository
{
    User? GetUserByUsername(string username);
    User CreateUser(string username, string passwordHash);
    bool UserExists(string username);
}