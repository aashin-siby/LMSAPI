using LMSAPI.Data;
using LMSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
     [HttpPost("addBook/{title}/{author}/{numberOfCopies}")]
     [Authorize(Roles = "Admin")]
     public ActionResult AddBook(string title, string author, int numberOfCopies)
     {
          if (numberOfCopies <= 0)
          {

               _logger.LogError("Enter a number greater than 0");
               return BadRequest("Number of copies must be greater than zero.");
          }
          var newBook = new Book
          {

               Title = title,
               Author = author,
               CopiesAvailable = numberOfCopies
          };

          _context.Books.Add(newBook);
          _context.SaveChanges();

          _logger.LogInformation("Admin added the new book succesfully");
          return Ok("Book added successfully.");
     }

     //Method to remove a Book - Admin 
     [HttpDelete("removeBook/{bookId}")]
     [Authorize(Roles = "Admin")]
     public ActionResult RemoveBook(int bookId)
     {
          var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
          if (book == null)
          {

               _logger.LogError("Book with bookId " + bookId + " not found");
               return NotFound("Book not found.");
          }
          _context.Books.Remove(book);
          _context.SaveChanges();
          _logger.LogInformation("Admin removed the " + book.Title + "book succesfully");
          return Ok("Book removed successfully.");
     }

     //Method to increase the book copies - Admin
     [HttpPost("increaseBookCopies/{bookId}/{count}")]
     [Authorize(Roles = "Admin")]
     public ActionResult IncreaseBookCopies(int bookId, int count)
     {
          var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
          if (book == null)
          {

               _logger.LogError("Book with bookId " + bookId + " not found");
               return NotFound("Book not found.");
          }
          book.CopiesAvailable += count;
          _context.SaveChanges();
          _logger.LogInformation("Admin added the " + book.Title + "count succesfully");
          return Ok("Book copies increased successfully.");
     }
}
