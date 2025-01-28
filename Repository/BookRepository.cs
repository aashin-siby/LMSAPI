using LMSAPI.Data;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;

namespace LMSAPI.Repository;
public class BookRepository : IBookRepository
{
     private readonly LibraryDbContext _dbContext;

     public BookRepository(LibraryDbContext dbContext)
     {
          _dbContext = dbContext;
     }

    public IEnumerable<Book> GetAllBooks()
    {
        return _dbContext.Books.ToList();
    }
     public Book GetBookById(int bookId)
     {
          return _dbContext.Books.Find(bookId);
     }

     public void UpdateBook(Book book)
     {
          _dbContext.Books.Update(book);
     }

     public void Save()
     {
          _dbContext.SaveChanges();
     }


}
