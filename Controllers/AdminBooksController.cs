using System.ComponentModel;
using LMSAPI.Data;
using LMSAPI.DTO;
using LMSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSAPI.Controllers;

//Controller which handle all the library methods that can be done by and for Admin
[Route("api/[controller]")]
[ApiController]
public class AdminBooksController : ControllerBase

{
     private readonly LibraryDbContext _context;
     private readonly ILogger<AdminBooksController> _logger;
     public AdminBooksController(LibraryDbContext context, ILogger<AdminBooksController> logger)
     {

          _context = context;
          _logger = logger;
     }
     //Method to add New Book - Admin
     [HttpPost("addBook/{title}/{author}/{imageUrl}/{description}/{numberOfCopies}")]
     [Authorize(Roles = "Admin")]
     public ActionResult AddBook(string title, string author, string imageUrl, string description, int numberOfCopies)
     {
          if (numberOfCopies <= 0)
          {

               _logger.LogError("Enter a number greater than 0");
               return BadRequest("Number of copies must be greater than zero.");
          }
          var newBook = new Book
          {

               Title = title,
               ImageUrl = imageUrl,
               Author = author,
               BookDescription = description,
               CopiesAvailable = numberOfCopies
          };

          _context.Books.Add(newBook);
          _context.SaveChanges();

          _logger.LogInformation("Admin added the new book successfully");
          return Ok("Book added successfully.");
     }

     //Method to remove a Book - Admin 
     [HttpDelete("removeBook/{bookId}")]
     [Authorize(Roles = "Admin")]
     public ActionResult RemoveBook(int bookId)
     {
          var book = _context.Books.FirstOrDefault(books => books.BookId == bookId);
          if (book == null)
          {

               _logger.LogError("Book with bookId " + bookId + " not found");
               return NotFound("Book not found.");
          }
          _context.Books.Remove(book);
          _context.SaveChanges();
          _logger.LogInformation("Admin removed the " + book.Title + "book successfully");
          return Ok("Book removed successfully.");
     }

     //Method to increase the book copies - Admin
     [HttpPost("increaseBookCopies/{bookId}/{count}")]
     [Authorize(Roles = "Admin")]
     public ActionResult IncreaseBookCopies(int bookId, int count)
     {
          var book = _context.Books.FirstOrDefault(books => books.BookId == bookId);
          if (book == null)
          {

               _logger.LogError("Book with bookId " + bookId + " not found");
               return NotFound("Book not found.");
          }
          book.CopiesAvailable += count;
          _context.SaveChanges();
          _logger.LogInformation("Admin added the " + book.Title + "count successfully");
          return Ok("Book copies increased successfully.");
     }


     //Method to get all the rental details - Admin
     [HttpGet("rentalDetails")]
     [Authorize(Roles = "Admin")]
     public ActionResult<IEnumerable<RentalDetailsDto>> GetRentalDetails()
     {
          try
          {
               var rentalDetails = _context.BorrowDetails
                                           .Include(borrowdetails => borrowdetails.User)
                                           .Include(borrowdetails => borrowdetails.Book)
                                           .Select(borrowdetails => new RentalDetailsDto
                                           {
                                                BorrowId = borrowdetails.BorrowId,
                                                BookId = borrowdetails.BookId,
                                                Title = borrowdetails.Book.Title,
                                                UserId = borrowdetails.UserId,
                                                Username = borrowdetails.User.Username,
                                                Payment = borrowdetails.Payment
                                           })
                                           .ToList();

               if (!rentalDetails.Any())
               {

                    _logger.LogError("Error retrieving rental details: No rental details found"); ;
                    return NotFound("No rental details found.");
               }

               _logger.LogInformation("Successfully retrieved rental details");
               return Ok(rentalDetails);
          }
          catch (Exception exception)
          {

               _logger.LogError("Error retrieving rental details: " + exception.Message);
               return StatusCode(500, "Internal server error: " + exception.Message);
          }
     }

}
