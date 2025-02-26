using LMSAPI.DTO;
using LMSAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSAPI.Controllers;

//Controller which handle all the library methods for User
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserBooksController : ControllerBase
{

     private readonly IUserBooksService _userBooksService;
     private readonly ILogger<UserBooksController> _logger;
     public UserBooksController(IUserBooksService userBooksService, ILogger<UserBooksController> logger)
     {

          _userBooksService = userBooksService;
          _logger = logger;
     }

     // Get all books
     [HttpGet("books")]
     [AllowAnonymous]
     public IActionResult GetAllBooks()
     {

          var books = _userBooksService.GetAllBooks();
          _logger.LogInformation($"Fetched {books.Count()} books from the database.");
          return Ok(books);
     }

     // Borrow book
     [HttpPost("borrow")]
     public IActionResult BorrowBook([FromBody] BorrowBookDto borrowBookDto)
     {
          if (!ModelState.IsValid)
          {

               _logger.LogError("Model binding of borrowBookDto not successful");
               return BadRequest(ModelState);
          }

          var userId = User.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
          if (userId == null)
          {

               _logger.LogError("User not found when trying to borrow Book");
               return Unauthorized("User not found in claims.");
          }

          borrowBookDto.UserId = int.Parse(userId);
          try
          {

               _logger.LogInformation($"Book borrowed successfully by user {borrowBookDto.UserId}");
               _userBooksService.BorrowBook(borrowBookDto);
               return Ok("Book borrowed successfully");
          }
          catch (ArgumentException argumentException)
          {
               _logger.LogError(argumentException, "Invalid data provided.");
               return BadRequest("Invalid request data.");
          }
          catch (InvalidOperationException invalidOperationException)
          {
               _logger.LogError(invalidOperationException, "Operation failed due to business logic constraints.");
               return BadRequest(invalidOperationException.Message);
          }
          catch (Exception exception)
          {
               _logger.LogError(exception, "Unexpected error occurred.");
               return StatusCode(500, "An internal error occurred. Please try again later.");
          }
     }

     // Return book
     // Return book
     [HttpPost("return")]
     public IActionResult ReturnBook([FromBody] ReturnBookDto returnBookDto)
     {
          if (!ModelState.IsValid)
          {
               _logger.LogError("Invalid returnBookDto model.");
               return BadRequest(ModelState);
          }

          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
          {
               _logger.LogError("Invalid or missing userId in claims.");
               return Unauthorized("Invalid userId.");
          }

          returnBookDto.UserId = parsedUserId;

          try
          {
               _userBooksService.ReturnBook(returnBookDto);
               _logger.LogInformation("User {UserId} returned book {BookId}.", parsedUserId, returnBookDto.BookId);
               return Ok("Book returned successfully.");
          }
          catch (InvalidOperationException invalidOperationException)
          {
               _logger.LogError(invalidOperationException, "Business logic error while returning book.");
               return BadRequest(invalidOperationException.Message);
          }
          catch (Exception exception)
          {
               _logger.LogError(exception, "Unexpected error while returning book.");
               return StatusCode(500, "An internal error occurred.");
          }
     }

     //Method to get the rental details of Users
     [HttpGet("rentals")]
     public IActionResult GetUserRentals()
     {

          var userId = User.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
          if (string.IsNullOrEmpty(userId))
          {

               _logger.LogError("User not found to retrieve his/her rental details");
               return Unauthorized("User ID not found in claims.");
          }

          try
          {

               var rentals = _userBooksService.GetUserRentals(int.Parse(userId));
               _logger.LogInformation($"Displayed rental details for user {userId}");
               return Ok(rentals);
          }
          catch (Exception exception)
          {

               _logger.LogError(exception, "Error occurred while fetching user rentals.");
               return StatusCode(500, "An internal error occurred while fetching rentals.");
          }
     }

}