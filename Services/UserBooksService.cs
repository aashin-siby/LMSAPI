using AutoMapper;
using LMSAPI.DTO;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;

namespace LMSAPI.Services;
//Service for Library methods that is related to User
public class UserBooksService
{
     private readonly IBookRepository _bookRepository;
     private readonly IBorrowDetailsRepository _borrowDetailsRepository;
     private readonly IRentalRepository _rentalRepository;

     public UserBooksService(IRentalRepository rentalRepository, IBookRepository bookRepository, IBorrowDetailsRepository borrowDetailsRepository)
     {
          _bookRepository = bookRepository;
          _borrowDetailsRepository = borrowDetailsRepository;
          _rentalRepository = rentalRepository;

     }

     /// Retrieves all books and maps them to BookDto.
     public IEnumerable<BookDto> GetAllBooks()
     {
          var books = _bookRepository.GetAllBooks();
          return books.Select(books => new BookDto
          {
               BookId = books.BookId,
               Title = books.Title,
               Author = books.Author,
               ImageUrl = books.ImageUrl,
               BookDescription = books.BookDescription,
               CopiesAvailable = books.CopiesAvailable
          });
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

          var borrowDetails = new BorrowDetails
          {
               Title = bookTitle,
               UserId = borrowBookDto.UserId,
               BookId = borrowBookDto.BookId,
               BorrowDate = borrowBookDto.BorrowDate,
               Payment = 100
          };

          _borrowDetailsRepository.AddBorrowDetails(borrowDetails);
          _borrowDetailsRepository.Save();
          _bookRepository.Save();
     }

     /// Allows a user to return a borrowed book.
     public void ReturnBook(ReturnBookDto returnBookDto)
     {
          var borrowDetails = _borrowDetailsRepository.GetBorrowDetailsByUserIdAndBookId(returnBookDto.UserId, returnBookDto.BorrowId);
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
          var rentals = _rentalRepository.GetUserRentals(userId);
          return rentals.Select(r => new BorrowDetailsDto
          {
               Title = r.Title,
               BorrowId = r.BorrowId,
               BookId = r.BookId,
               BorrowDate = r.BorrowDate,
               ReturnDate = r.ReturnDate,
               Payment = r.Payment,
          }).ToList();
     }
}