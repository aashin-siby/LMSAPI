using LMSAPI.DTO;
using LMSAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSAPI.Controllers;

//Controller which handle all the library methods that can be done by and for User
[Route("api/[controller]")]
[ApiController]
public class UserBooksController : ControllerBase
{

     private readonly UserBooksService _userBooksService;
     private readonly ILogger<UserBooksController> _logger;
     public UserBooksController(UserBooksService userBooksService, ILogger<UserBooksController> logger)
     {

          _userBooksService = userBooksService;
          _logger = logger;
     }

     //Method to get all the books 
     [HttpGet("books")]
     public IActionResult GetAllBooks()
     {

          var books = _userBooksService.GetAllBooks();
          _logger.LogInformation("Succesfully loaded the Books");
          return Ok(books);
     }

     //Method to borrow the particular book
     [HttpPost("borrow")]
     [Authorize]
     public IActionResult BorrowBook([FromBody] BorrowBookDto borrowBookDto)
     {
          if (!ModelState.IsValid)
          {

               _logger.LogError("Model binding of borrowBookDto not successful");
               return BadRequest(ModelState);
          }

          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (userId == null)
          {

               _logger.LogError("User not found when trying to borrow Book");
               return Unauthorized("User not found in claims.");
          }

          borrowBookDto.UserId = int.Parse(userId);
          try
          {

               _logger.LogInformation("Book borrowed successfully by" + borrowBookDto.UserId);
               _userBooksService.BorrowBook(borrowBookDto);
               return Ok("Book borrowed successfully");
          }
          catch (Exception exception)
          {

               _logger.LogError(exception, "Error occurred while borrowing book.");
               return BadRequest("An error occurred while borrowing the book. Please try again later.");
          }
     }

     //Method to return the book which is borrowed
     [HttpPost("return")]
     [Authorize]
     public IActionResult ReturnBook([FromBody] ReturnBookDto returnBookDto)
     {

          if (!ModelState.IsValid)
          {

               _logger.LogError("Model binding of returnBookDto not successful");
               return BadRequest(ModelState);
          }
          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (userId == null)
          {

               _logger.LogError("User not found when trying to return Book");
               return Unauthorized("User not found in claims.");
          }
          returnBookDto.UserId = int.Parse(userId);
          try
          {

               _logger.LogInformation("Book returned successfully by" + returnBookDto.UserId);
               _userBooksService.ReturnBook(returnBookDto);
               return Ok("Book returned successfully");
          }
          catch (Exception exception)
          {

               _logger.LogError(exception, "Error occurred while returning book.");
               return BadRequest("An error occurred while returning the book. Please try again later.");
          }
     }

     //Method to get the rental details of individual Users
     [HttpGet("rentals")]
     [Authorize]

     public IActionResult GetUserRentals()
     {

          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (string.IsNullOrEmpty(userId))
          {

               _logger.LogError("User not found to retrive his/her rental details");
               return Unauthorized("User ID not found in claims.");
          }

          try
          {

               var rentals = _userBooksService.GetUserRentals(int.Parse(userId));
               _logger.LogInformation("Displayed the rental details of "+ userId);
               return Ok(rentals);
          }
          catch (Exception exception)
          {

               _logger.LogError(exception, "Error occurred while fetching user rentals.");
               return BadRequest("An error occurred while fetching rentals. Please try again later.");
          }
     }

}