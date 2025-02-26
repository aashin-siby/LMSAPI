using AutoMapper;
using LMSAPI.DTO;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;

namespace LMSAPI.Services;

//Service for Library methods that is related to User
public class UserBooksService : IUserBooksService
{
     private readonly IBookRepository _bookRepository;
     private readonly IBorrowDetailsRepository _borrowDetailsRepository;
     private readonly IMapper _mapper;

     public UserBooksService(IBookRepository bookRepository, IBorrowDetailsRepository borrowDetailsRepository, IMapper mapper)
     {
          _bookRepository = bookRepository;
          _borrowDetailsRepository = borrowDetailsRepository;
          _mapper = mapper;

     }

     /// Retrieves all books and maps them to BookDto.
     public IEnumerable<BookDto> GetAllBooks()
     {
          var books = _bookRepository.GetAllBooks();
         return _mapper.Map<IEnumerable<BookDto>>(books);

     }

     /// Allows a user to borrow a book.
     public void BorrowBook(BorrowBookDto borrowBookDto)
     {
          var book = _bookRepository.GetBookById(borrowBookDto.BookId);
          var bookTitle = _bookRepository.GetBookTitleById(borrowBookDto.BookId);

          if (book == null || book.CopiesAvailable < 1)
               throw new Exception("Book not available");

          book.CopiesAvailable -= 1;

          _bookRepository.UpdateBook(book);
          
          var borrowDetails = _mapper.Map<BorrowDetails>(borrowBookDto);
          borrowDetails.Title = bookTitle;

          _borrowDetailsRepository.AddBorrowDetails(borrowDetails);
          _borrowDetailsRepository.Save();
          _bookRepository.Save();
     }

     /// Allows a user to return a borrowed book.
     public void ReturnBook(ReturnBookDto returnBookDto)
     {
          var borrowDetails = _borrowDetailsRepository.GetBorrowDetailsByUserIdAndBorrowId(returnBookDto.UserId, returnBookDto.BorrowId);
          if (borrowDetails == null || borrowDetails.ReturnDate != null)
               throw new Exception("Borrow details not found or book already returned");

          borrowDetails.ReturnDate = returnBookDto.ReturnDate;
          var daysBorrowed = (returnBookDto.ReturnDate - borrowDetails.BorrowDate).Days;
          if (daysBorrowed > 10)
          {
               borrowDetails.Payment = 100 + (daysBorrowed - 10) * 5;
          }
          else
          {
               borrowDetails.Payment = 100;
          }

          var book = _bookRepository.GetBookById(borrowDetails.BookId);
          book.CopiesAvailable += 1;
          _bookRepository.UpdateBook(book);

          _borrowDetailsRepository.UpdateBorrowDetails(borrowDetails);
          _borrowDetailsRepository.Save();
          _bookRepository.Save();
     }

     /// Retrieves all rental details for a specific user
     public IEnumerable<BorrowDetailsDto> GetUserRentals(int userId)
     {
          var rentals = _borrowDetailsRepository.GetUserRentals(userId);

          return _mapper.Map<IEnumerable<BorrowDetailsDto>>(rentals);
         
     }
}