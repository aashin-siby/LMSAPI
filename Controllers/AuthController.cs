
using LMSAPI.Data;
using LMSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using LMSAPI.exceptions;
namespace LMSAPI.Controllers;

//Controller which control authentication and authorization 
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LibraryDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(LibraryDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    //Method to Register a new user
    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        try
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully!");
        }
        catch (DbUpdateException dbexception)
        {
            return StatusCode(500, $"An error occurred while registering the user. {dbexception}");
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"An unexpected error occurred. {exception}.");
        }
    }

    //Method to login and generate a JWT token
    [HttpPost("login")]
    public IActionResult Login([FromBody] User login)
    {
        try
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == login.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("userId", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
        catch (Exception exception )
        {
            return StatusCode(500, $"An unexpected error occurred. {exception}.");
        }
    }
}

