using Microsoft.AspNetCore.Mvc;
using Common.Models.DTOs;
using AuthService.Business.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody]LoginDto login)
    {
        var result = _authService.Register(login);
        if (result.Contains("Successful"))
        {
            return Ok(new { message = result });
        }
        return BadRequest(new { message = result });
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody]LoginDto login)
    {
        var user = _authService.Login(login);
        if (user == null)
        {
            return BadRequest(new { message = "Invalid Credential" });
        }

        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "Auth_Service_Microservice_Secret_Key_2024_Secure");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { message = "Login successful", user = user.Username, token = tokenString, userId = user.Id });
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { service = "AuthService", status = "Running", timestamp = DateTime.UtcNow });
    }
}