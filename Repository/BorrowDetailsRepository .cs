using LMSAPI.Data;
using LMSAPI.DTO;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LMSAPI.Repository;
//Repository to abastract the LibraryDbContext with BorrowDetails
public class BorrowDetailsRepository : IBorrowDetailsRepository
{
    private readonly LibraryDbContext _context;
    public BorrowDetailsRepository(LibraryDbContext context)
    {
        _context = context;
    }

    /// Adds a new borrow record to the database.
    public void AddBorrowDetails(BorrowDetails borrowDetails)
    {

        _context.BorrowDetails.Add(borrowDetails);
    }

    /// Retrieves all borrow details for a specific user.
    public IEnumerable<BorrowDetails> GetBorrowDetailsByUserId(int userId)
    {
        return _context.BorrowDetails
                       .Include(borrowDetails => borrowDetails.Book)
                       .Where(borrowDetails => borrowDetails.UserId == userId)
                       .ToList();
    }

    /// Retrieves a specific borrow record for a user based on borrow ID.
    public BorrowDetails GetBorrowDetailsByUserIdAndBorrowId(int userId, int borrowId)
    {
        return _context.BorrowDetails
                      .Include(borrowDetails => borrowDetails.Book)
                      .FirstOrDefault(borrowDetails => borrowDetails.UserId == userId && borrowDetails.BorrowId == borrowId);
    }

    /// Retrieves all rentals for a specific user.
    public IEnumerable<BorrowDetails> GetUserRentals(int userId)
    {
        return _context.BorrowDetails
            .Include(borrowDetails => borrowDetails.Book)
            .Where(borrowDetails => borrowDetails.UserId == userId)
            .OrderByDescending(borrowDetails => borrowDetails.BorrowDate)
            .ToList();
    }


    /// Updates an existing borrow record in the database.
    public void UpdateBorrowDetails(BorrowDetails borrowDetails)
    {
        _context.BorrowDetails.Update(borrowDetails);
    }

    /// Saves all changes made to the database context.
    public void Save()
    {
        _context.SaveChanges();
    }

    // Retrieves a list of rental details asynchronously.
    public async Task<IEnumerable<RentalDetailsDto>> GetRentalDetailsAsync()
    {
        return await _context.BorrowDetails
            .Include(borrowDetails => borrowDetails.User)
            .Include(borrowDetails => borrowDetails.Book)
            .Select(borrowDetails => new RentalDetailsDto
            {
                BorrowId = borrowDetails.BorrowId,
                BookId = borrowDetails.BookId,
                Title = borrowDetails.Book.Title,
                UserId = borrowDetails.UserId,
                Username = borrowDetails.User.Username,
                ReturnDate = borrowDetails.ReturnDate,
                Payment = borrowDetails.Payment
            })
            .ToListAsync();
    }
}
