using LMSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LMSAPI.Repository.IRepository;
using LMSAPI.DTO;
using AutoMapper;
using LMSAPI.Data;
using System.Text.Json;


namespace LMSAPI.Controllers
{
    // Controller to handle authentication and authorization
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        private readonly LibraryDbContext _context;

        public AuthController(LibraryDbContext context, IUserRepository userRepository, IConfiguration configuration, IMapper mapper, ILogger<AuthController> logger)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
        }

        // Method to register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            try
            {
                // Hash the user's password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
                var user = _mapper.Map<User>(userRegister);
                user.Password = hashedPassword;

                // Add the user to the repository
                await _userRepository.AddUserAsync(user);

                return Ok("User registered successfully!");
            }
            catch (DbUpdateException dbException)
            {
                _logger.LogError(dbException, "An error occurred while registering the user.");
                return StatusCode(500, "An error occurred while registering the user. Please try again later.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unexpected error occurred during user registration.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // Method to login an existing user
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                _logger.LogInformation("Login attempt for username: {Username}", request.Username);

                var user = _context.Users.FirstOrDefault(u =>
                    EF.Functions.Collate(u.Username, "SQL_Latin1_General_CP1_CS_AS") == request.Username);

                if (user == null)
                {
                    _logger.LogWarning("Login failed: User not found for username: {Username}", request.Username);
                    return Unauthorized(new { success = false, message = "Invalid username or password!" });
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    _logger.LogWarning("Login failed: Invalid password for username: {Username}", request.Username);
                    return Unauthorized(new { success = false, message = "Invalid username or password!" });
                }

                var token = GenerateJwtToken(user);

                _logger.LogInformation("Login successful for username: {Username}", request.Username);
                var jsonResponse = new { token, success = true, role = user.Role, message = "Login successful!" };
                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for username: {Username}", request.Username);
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }

        //Method to generate the token while login
        private string GenerateJwtToken(User user)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

                var claims = new[]
                {
                    new Claim("userId", user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256
                    )
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the JWT token for user: {Username}", user.Username);
                throw;
            }
        }
    }
}