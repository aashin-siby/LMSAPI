using LMSAPI.Data;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;

namespace LMSAPI.Repository;
//Repository to abastract the LibraryDbContext with Book
public class BookRepository : IBookRepository
{
     private readonly LibraryDbContext _dbContext;

     public BookRepository(LibraryDbContext dbContext)
     {

          _dbContext = dbContext;
     }

     /// Retrieves all books from the database.
     public IEnumerable<Book> GetAllBooks()
     {

          return _dbContext.Books.ToList();
     }

     /// Retrieves a book by its ID.
     public Book GetBookById(int bookId)
     {

          return _dbContext.Books.Find(bookId);
     }

     /// Retrieves the title of a book based on its ID.
     public string? GetBookTitleById(int bookId)
     {

          var book = _dbContext.Books.Find(bookId);
          return book?.Title;
     }

     /// Updates an existing book in the database.
     public void UpdateBook(Book book)
     {
          _dbContext.Books.Update(book);
     }

    /// Saves all changes made to the database context.
     public void Save()
     {
          _dbContext.SaveChanges();
     }


}
