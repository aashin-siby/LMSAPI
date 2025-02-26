using LMSAPI.DTO;
using LMSAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSAPI.Controllers;

//Controller which handle all the library methods that can be done by and for Admin
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminBooksController : ControllerBase

{
     private readonly IAdminBookService _adminBookService;
     private readonly ILogger<AdminBooksController> _logger;
     public AdminBooksController(IAdminBookService adminBookService, ILogger<AdminBooksController> logger)
     {

          _adminBookService = adminBookService;
          _logger = logger;

     }

     //POST: Method to add New Book - Admin
     [HttpPost("addBook")]
     public async Task<ActionResult> AddBook([FromBody] BookDto bookDto)
     {
          try
          {
               await _adminBookService.AddBookAsync(bookDto);
               return Ok("Book added successfully.");
          }
          catch (ArgumentException argumentException)
          {
               _logger.LogError(argumentException.Message);
               return BadRequest(argumentException.Message);
          }
     }

     //Method to remove a Book - Admin 
     [HttpDelete("removeBook/{bookId}")]
     public async Task<ActionResult> RemoveBook(int bookId)
     {
          var result = await _adminBookService.RemoveBookAsync(bookId);
          if (!result)
               return NotFound("Book not found.");

          return Ok("Book removed successfully.");
     }

     //Method to increase the book copies - Admin
     [HttpPost("increaseBookCopies/{bookId}/{count}")]
     public async Task<ActionResult> IncreaseBookCopies(int bookId, int count)
     {
          var result = await _adminBookService.IncreaseBookCopiesAsync(bookId, count);
          if (!result)
               return NotFound("Book not found.");

          return Ok("Book copies increased successfully.");
     }


     //Method to get all the rental details - Admin
     [HttpGet("rentalDetails")]
     public async Task<ActionResult<IEnumerable<RentalDetailsDto>>> GetRentalDetails()
     {
          var rentalDetails = await _adminBookService.GetRentalDetailsAsync();
          if (!rentalDetails.Any())
               return NotFound("No rental details found.");

          return Ok(rentalDetails);
     }

}
