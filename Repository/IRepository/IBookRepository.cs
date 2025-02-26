using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;

// Repository interface defining all methods for managing books
public interface IBookRepository
{
     // User functionalities
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


     // Admin functionalities
     //Add new Book 
     Task AddBookAsync(Book book);
     //Remove an existing Book 
     Task RemoveBookAsync(Book book);
     //Increase the copy of Books
     Task IncreaseBookCopiesAsync(Book book, int count);
}