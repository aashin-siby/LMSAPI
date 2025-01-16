using LMSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LMSAPI.Data;
using System.Linq;
using System.Security.Claims;

namespace LMSAPI.Controllers;

//Controller which defines the Library Modules
[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{

     private readonly LibraryDbContext _context;

     public LibraryController(LibraryDbContext context)
     {
          _context = context;
     }

     //Method to get all the Books 
     [HttpGet("viewBooks")]
     [Authorize]
     public ActionResult<IEnumerable<Book>> ViewBooks()
     {
          return Ok(_context.Books.ToList());
     }

     //Method to borrow a book with BookId
     [HttpPost("borrowBook/{id}")]
     [Authorize]
     public ActionResult BorrowBook(int id)
     {
          var book = _context.Books.FirstOrDefault(b => b.BookId == id);
          if (book == null || book.CopiesAvailable <= 0)
               return BadRequest("Book not available.");

          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (userId == null)
               return Unauthorized();

          book.CopiesAvailable--;
          _context.SaveChanges();

          return Ok("Book borrowed successfully.");
     }

     //Method to return the borrowed book with BookId
     [HttpPost("returnBook/{id}")]
     [Authorize]
     public ActionResult ReturnBook(int id)
     {
          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (userId == null)
               return Unauthorized();

          var book = _context.Books.FirstOrDefault(b => b.BookId == id);
          if (book != null)
          {
               book.CopiesAvailable++;
               _context.SaveChanges();
               return Ok("Book returned successfully.");
          }

          return BadRequest("Book not found.");
     }

     //Method to add New Book - Admin
     [HttpPost("addBook")]
     [Authorize(Roles = "Admin")]
     public ActionResult AddBook([FromBody] Book newBook)
     {
          var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
          if (userRole != "Admin")
          {
               return Unauthorized("User is not an admin.");
          }

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
