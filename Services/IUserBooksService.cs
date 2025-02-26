using LMSAPI.DTO;

namespace LMSAPI.Services;

// Interface defining user-related book management operations.
public interface IUserBooksService
{
     /// Retrieves all books available in the library.
     IEnumerable<BookDto> GetAllBooks();

     /// Allows a user to borrow a book.
     void BorrowBook(BorrowBookDto borrowBookDto);

     /// Allows a user to return a borrowed book.
     void ReturnBook(ReturnBookDto returnBookDto);


     /// Retrieves all rental details for a specific user.
     IEnumerable<BorrowDetailsDto> GetUserRentals(int userId);

}