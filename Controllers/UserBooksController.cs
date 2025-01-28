using AutoMapper;
using LMSAPI.DTO;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;
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
     [HttpGet("books")]
     public IActionResult GetAllBooks()
     {
          var books = _userBooksService.GetAllBooks();
          return Ok(books);
     }

     [HttpPost("borrow")]
     [Authorize]
     public IActionResult BorrowBook([FromBody] BorrowBookDto borrowBookDto)
     {
          if (!ModelState.IsValid)
          {
               return BadRequest(ModelState);
          }

          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (userId == null)
          {
               return Unauthorized("User ID not found in claims.");
          }

          borrowBookDto.UserId = int.Parse(userId);
          try
          {
               _userBooksService.BorrowBook(borrowBookDto);
               return Ok("Book borrowed successfully");
          }
          catch (Exception ex)
          {
               // Log the exception (assuming you have a logger)
               _logger.LogError(ex, "Error occurred while borrowing book.");

               return BadRequest("An error occurred while borrowing the book. Please try again later.");
          }
     }
     [HttpPost("return")]
     [Authorize]
     public IActionResult ReturnBook([FromBody] ReturnBookDto returnBookDto)
     {
          if (!ModelState.IsValid)
          {
               return BadRequest(ModelState);
          }

          var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (userId == null)
          {
               return Unauthorized("User ID not found in claims.");
          }

          returnBookDto.UserId = int.Parse(userId);
          try
          {
               _userBooksService.ReturnBook(returnBookDto);
               return Ok("Book returned successfully");
          }
          catch (Exception ex)
          {
               // Log the exception (assuming you have a logger)
               _logger.LogError(ex, "Error occurred while returning book.");

               return BadRequest("An error occurred while returning the book. Please try again later.");
          }
     }

     [HttpGet("bill")]
     [Authorize]
     public IActionResult ViewBill()
     {
          var claimsUserId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (claimsUserId == null)
          {
               return Unauthorized("User ID not found in claims.");
          }

          try
          {
               var userId = int.Parse(claimsUserId);
               var bills = _userBooksService.ViewBill(userId);
               return Ok(bills);
          }
          catch (Exception ex)
          {
               _logger.LogError(ex, "Error occurred while viewing bill.");
               return BadRequest("An error occurred while viewing the bill. Please try again later.");
          }
     }


}