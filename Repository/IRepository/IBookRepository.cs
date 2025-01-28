using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;
//Repository on all the methods for the Book
public interface IBookRepository
{
     IEnumerable<Book> GetAllBooks();
     Book GetBookById(int bookId);
     void UpdateBook(Book book);
     void Save();
}