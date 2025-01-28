using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;
public interface IBookRepository
{
     IEnumerable<Book> GetAllBooks();
     Book GetBookById(int bookId);
     void UpdateBook(Book book);
     void Save();
}