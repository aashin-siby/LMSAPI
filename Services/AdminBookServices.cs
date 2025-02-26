using AutoMapper;
using LMSAPI.DTO;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;

namespace LMSAPI.Services;

// Service class responsible for handling admin-related book operations.
public class AdminBookService : IAdminBookService
{
     private readonly IBookRepository _bookRepository;
     private readonly IBorrowDetailsRepository _borrowDetailsRepository;
     private readonly IMapper _mapper;
     private readonly ILogger<AdminBookService> _logger;

     public AdminBookService(
         IBookRepository bookRepository,
         IBorrowDetailsRepository borrowDetailsRepository,
         IMapper mapper,
         ILogger<AdminBookService> logger)
     {
          _bookRepository = bookRepository;
          _borrowDetailsRepository = borrowDetailsRepository;
          _mapper = mapper;
          _logger = logger;
     }

     // Adds a new book to the system.
     public async Task AddBookAsync(BookDto bookDto)
     {
          if (bookDto == null || bookDto.CopiesAvailable <= 0)
          {
               _logger.LogError("Invalid book data received.");
               throw new ArgumentException("Invalid book data or copies must be greater than zero.");
          }

          var newBook = _mapper.Map<Book>(bookDto);
          await _bookRepository.AddBookAsync(newBook);
          _logger.LogInformation("Admin added a new book successfully.");
     }

     // Removes a book from the system by its ID.
     public async Task<bool> RemoveBookAsync(int bookId)
     {
          var book = _bookRepository.GetBookById(bookId);
          if (book == null)
          {
               _logger.LogError($"Book with ID {bookId} not found.");
               return false;
          }

          await _bookRepository.RemoveBookAsync(book);
          _logger.LogInformation($"Book with ID {bookId} removed successfully.");
          return true;
     }

     // Increases the available copies of a book by a specified count.
     public async Task<bool> IncreaseBookCopiesAsync(int bookId, int count)
     {
          var book = _bookRepository.GetBookById(bookId);
          if (book == null)
          {
               _logger.LogError($"Book with ID {bookId} not found.");
               return false;
          }

          await _bookRepository.IncreaseBookCopiesAsync(book, count);
          _logger.LogInformation($"Admin increased copies of {book.Title} by {count}.");
          return true;
     }

     // Retrieves rental details for all borrowed books.
     public async Task<IEnumerable<RentalDetailsDto>> GetRentalDetailsAsync()
     {
          var rentalDetails = await _borrowDetailsRepository.GetRentalDetailsAsync();
          return rentalDetails;
     }
}
