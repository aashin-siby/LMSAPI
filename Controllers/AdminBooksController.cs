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

          return Ok("Book added successfully.");
     }

     //Method to remove a Book - Admin 
     [HttpDelete("removeBook/{id}")]
     [Authorize(Roles = "Admin")]
     public ActionResult RemoveBook(int id)
     {
          var book = _context.Books.FirstOrDefault(b => b.BookId == id);
          if (book == null)
               return NotFound("Book not found.");

          _context.Books.Remove(book);
          _context.SaveChanges();
          return Ok("Book removed successfully.");
     }

     //Method to increase the book copies - Admin
     [HttpPost("increaseBookCopies/{id}/{count}")]
     [Authorize(Roles = "Admin")]
     public ActionResult IncreaseBookCopies(int id, int count)
     {
          var book = _context.Books.FirstOrDefault(b => b.BookId == id);
          if (book == null)
               return NotFound("Book not found.");

          book.CopiesAvailable += count;
          _context.SaveChanges();
          return Ok("Book copies increased successfully.");
     }
}
