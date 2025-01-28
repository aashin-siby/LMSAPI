using AutoMapper;
using LMSAPI.DTO;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;

namespace LMSAPI.Services;
public class UserBooksService
{
     private readonly IBookRepository _bookRepository;
     private readonly IBorrowDetailsRepository _borrowDetailsRepository;

     private readonly IMapper _mapper;
     // private readonly ILogger _logger;
     //  ILogger logger,
     public UserBooksService(IMapper mapper, IBookRepository bookRepository, IBorrowDetailsRepository borrowDetailsRepository)
     {
          // _logger = logger;
          _mapper = mapper;
          _bookRepository = bookRepository;
          _borrowDetailsRepository = borrowDetailsRepository;

     }

     public IEnumerable<BookDto> GetAllBooks()
     {
          var books = _bookRepository.GetAllBooks();
          return books.Select(b => new BookDto
          {
               BookId = b.BookId,
               Title = b.Title,
               Author = b.Author,
               CopiesAvailable = b.CopiesAvailable
          });
     }
     public void BorrowBook(BorrowBookDto borrowBookDto)
     {
          var book = _bookRepository.GetBookById(borrowBookDto.BookId);
          if (book == null || book.CopiesAvailable < 1)
               throw new Exception("Book not available");

          book.CopiesAvailable -= 1;
          _bookRepository.UpdateBook(book);

          var borrowDetails = new BorrowDetails
          {
               UserId = borrowBookDto.UserId,
               BookId = borrowBookDto.BookId,
               BorrowDate = borrowBookDto.BorrowDate,
               Payment = 100
          };

          _borrowDetailsRepository.AddBorrowDetails(borrowDetails);
          _borrowDetailsRepository.Save();
          _bookRepository.Save();
     }

     public void ReturnBook(ReturnBookDto returnBookDto)
     {
          var borrowDetails = _borrowDetailsRepository.GetBorrowDetailsByUserIdAndBookId(returnBookDto.UserId, returnBookDto.BookId);
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

          var book = _bookRepository.GetBookById(returnBookDto.BookId);
          book.CopiesAvailable += 1;
          _bookRepository.UpdateBook(book);

          _borrowDetailsRepository.UpdateBorrowDetails(borrowDetails);
          _borrowDetailsRepository.Save();
          _bookRepository.Save();
     }

     public IEnumerable<BorrowDetailsDto> ViewBill(int userId)
     {
          var borrowDetailsList = _borrowDetailsRepository.GetBorrowDetailsByUserId(userId);
          var bills = borrowDetailsList.Select(b => _mapper.Map<BorrowDetailsDto>(b));
          return bills;
     }
}