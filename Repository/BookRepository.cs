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

     // Retrieves all books from the database.
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

     // Admin Methods

     // Adds a new book to the database
     public async Task AddBookAsync(Book book)
     {
          await _dbContext.Books.AddAsync(book);
          await _dbContext.SaveChangesAsync();
     }

     // Removes an existing book from the database
     public async Task RemoveBookAsync(Book book)
     {
          _dbContext.Books.Remove(book);
          await _dbContext.SaveChangesAsync();
     }

     // Increases the available copies of a book and updates the database 
     public async Task IncreaseBookCopiesAsync(Book book, int count)
     {
          book.CopiesAvailable += count;
          await _dbContext.SaveChangesAsync();
     }

}
