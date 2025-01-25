using AutoMapper;
using LMSAPI.DTO;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSAPI.Controllers;

//Controller which handle all the library methods that can be done by and for User
[Route("api/[controller]")]
[ApiController]
public class UserBooksController : ControllerBase
{
     private readonly IGenericRepository<Book> _bookRepository;

     private readonly IMapper _mapper;
     private readonly ILogger _logger;
     public UserBooksController(IMapper mapper, ILogger logger, IGenericRepository<Book> bookRepository)
     {
          _logger = logger;
          _mapper = mapper;
          _bookRepository = bookRepository;

     }
     [HttpGet("viewbooks")]
     [Authorize]
     public async Task<ActionResult<List<BookDto>>> GetBooks(int page = 1, int pageSize = 10)
     {
          var books = await _bookRepository.GetAll(page, pageSize);
          return Ok(books);
     }

}