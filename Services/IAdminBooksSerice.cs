using LMSAPI.DTO;

namespace LMSAPI.Services;

// Interface defining admin-related book management operations.
public interface IAdminBookService
{
     // Adds a new book to the system.
     Task AddBookAsync(BookDto bookDto);

     // Removes a book from the system by its ID.
     Task<bool> RemoveBookAsync(int bookId);

     // Increases the available copies of a book by a specified count.
     Task<bool> IncreaseBookCopiesAsync(int bookId, int count);

     // Retrieves rental details for all borrowed books.
     Task<IEnumerable<RentalDetailsDto>> GetRentalDetailsAsync();
}
