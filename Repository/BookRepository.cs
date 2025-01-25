using LMSAPI.Data;
using LMSAPI.DTO;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LMSAPI.Repository;

public class BookRepository : IGenericRepository<BookDto>
{
     private readonly LibraryDbContext _dbContext;

     public BookRepository(LibraryDbContext dbContext)
     {
          _dbContext = dbContext;
     }

    public async Task<List<BookDto>> GetAll(int page, int pageSize)
     {
          return await _dbContext.Set<Book>()
              .Skip((page - 1) * pageSize)
              .Take(pageSize)
              .Select(book => new BookDto
              {
                   BookId = book.BookId,
                   Title = book.Title,
                   Author = book.Author,
                   CopiesAvailable = book.CopiesAvailable

              })
              .ToListAsync();
     }

   
}
