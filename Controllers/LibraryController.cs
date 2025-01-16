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
     public ActionResult<IEnumerable<Book>> ViewBooks(int pageNumber = 1, int pageSize = 10)
     {
          if (pageNumber <= 0 || pageSize <= 0)
          {
               return BadRequest("Page number and page size must be greater than zero.");
          }

          // Calculate the number of books to skip
          int skip = (pageNumber - 1) * pageSize;

          // Fetch the paginated list of books
          var paginatedBooks = _context.Books
                                       .Skip(skip)
                                       .Take(pageSize)
                                       .ToList();

          // Return the paginated list of books
          return Ok(paginatedBooks);
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
     [HttpPost("addBook/{title}/{author}/{numberOfCopies}")]
[Authorize(Roles = "Admin")]
public ActionResult AddBook(string title, string author, int numberOfCopies)
{
    // Validate numberOfCopies
    if (numberOfCopies <= 0)
    {
        return BadRequest("Number of copies must be greater than zero.");
    }

    // Create a new book instance
    var newBook = new Book
    {
        Title = title,
        Author = author,
        CopiesAvailable = numberOfCopies
    };

    // Add the book to the database
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
