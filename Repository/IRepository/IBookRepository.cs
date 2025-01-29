using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;
// Repository interface defining all methods for managing books
public interface IBookRepository
{

     // Retrieves all books from the database.
     IEnumerable<Book> GetAllBooks();

     /// Retrieves a book by its unique identifier.
     Book GetBookById(int bookId);

     /// Retrieves the title of a book based on its ID.
     string? GetBookTitleById(int bookId);

     /// Updates an existing book in the database.
     void UpdateBook(Book book);

     /// Saves all changes made to the database context.
     void Save();
}