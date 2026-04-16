using System.Security.Cryptography;
using System.Text;
using AuthService.Business.Interfaces;
using AuthService.Data.Entities;
using AuthService.Data.Interfaces;
using Common.Models.DTOs;

namespace AuthService.Business.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public string Register(LoginDto loginDto)
    {
        if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
        {
            return "Username and Password are required";
        }

        if (_authRepository.UserExists(loginDto.Username))
        {
            return "Username already exists";
        }

        var passwordHash = HashPassword(loginDto.Password);
        _authRepository.CreateUser(loginDto.Username, passwordHash);

        return "Registration Successful";
    }

    public User? Login(LoginDto loginDto)
    {
        var user = _authRepository.GetUserByUsername(loginDto.Username);
        
        if (user == null)
        {
            return null;
        }

        var passwordHash = HashPassword(loginDto.Password);
        
        if (user.PasswordHash != passwordHash)
        {
            return null;
        }

        return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}