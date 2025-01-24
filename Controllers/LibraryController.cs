using LMSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LMSAPI.Data;
using System.Linq;
using System.Security.Claims;
using LMSAPI.DTO;
using LMSAPI.Repository.IRepository;
using AutoMapper;

namespace LMSAPI.Controllers;

//Controller which defines the Library Modules
[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{

     private readonly LibraryDbContext _context;
     private readonly IBorrowRepository _borrowRepository;
     private readonly IMapper _mapper;

     public LibraryController(LibraryDbContext context, IBorrowRepository borrowRepository, IMapper mapper)
     {
          _context = context;
          _borrowRepository = borrowRepository;
          _mapper = mapper;
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
          int skip = (pageNumber - 1) * pageSize;
          var paginatedBooks = _context.Books
                                       .Skip(skip)
                                       .Take(pageSize)
                                       .ToList();

          return Ok(paginatedBooks);
     }

     //Method to borrow a book with BookId
     [HttpPost("borrowBook/{id}")]
     [Authorize]
     public async Task<IActionResult> BorrowBook([FromBody] BorrowBookDto borrowBookDto)
     {
          var book = _context.Books.FirstOrDefault(b => b.BookId == borrowBookDto.BookId);
          if (book == null || book.CopiesAvailable <= 0)
               return BadRequest("Book not available.");

          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (userId == null)
               return Unauthorized();

          book.CopiesAvailable--;
          _context.SaveChanges();

          var borrowDetails = new BorrowDetails
          {
               UserId = int.Parse(userId),
               BookId = book.BookId,
               BorrowDate = borrowBookDto.BorrowDate
          };

          await _borrowRepository.BorrowBookAsync(borrowDetails);

          return Ok("Book borrowed successfully.");
     }
     // add date of return, and borrow
     //Method to return the borrowed book with BookId
     [HttpPost("returnBook/{id}")]
     [Authorize]
     public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
     {
          try
          {
               var borrowDetails = await _borrowRepository.ReturnBookAsync(returnBookDto.BorrowId, returnBookDto.ReturnDate);

               var book = _context.Books.FirstOrDefault(b => b.BookId == borrowDetails.BookId);
               if (book != null)
               {
                    book.CopiesAvailable++;
                    _context.SaveChanges();
               }

               return Ok(new { Message = "Book returned successfully!", Penalty = borrowDetails.Penalty });
          }
          catch (Exception ex)
          {
               return BadRequest(ex.Message);
          }
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
