using AuthService.Data.Entities;
using Common.Models.DTOs;

namespace AuthService.Business.Interfaces;

public interface IAuthService
{
    string Register(LoginDto loginDto);
    User? Login(LoginDto loginDto);
}