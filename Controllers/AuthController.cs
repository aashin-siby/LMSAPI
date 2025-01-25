
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
using LMSAPI.Repository.IRepository;
using LMSAPI.DTO;
using AutoMapper;
namespace LMSAPI.Controllers;

//Controller which control authentication and authorization 
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthController(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    //Method to Register a new user
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
                var user = _mapper.Map<User>(userRegister);
                user.Password = hashedPassword;

                await _userRepository.AddUserAsync(user);

                return Ok("User registered successfully!");
            }
            catch (DbUpdateException dbException)
            {
                return StatusCode(500, $"An error occurred while registering the user. {dbException}");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"An unexpected error occurred. {exception}.");
            }
        }

    //Method to login and generate a JWT token
    [HttpPost("login")]
   public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(userLogin.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
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
                        // new Claim(ClaimTypes.Role, user.Role)
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
            catch (Exception exception)
            {
                return StatusCode(500, $"An unexpected error occurred. {exception}.");
            }
        }
}

