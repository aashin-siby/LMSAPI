using LMSAPI.Data;
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
                       .Include(b => b.Book)
                       .Where(b => b.UserId == userId)
                       .ToList();
    }

    /// Retrieves a specific borrow record for a user based on borrow ID.
    public BorrowDetails GetBorrowDetailsByUserIdAndBookId(int userId, int borrowId)
    {
        return _context.BorrowDetails
                      .Include(b => b.Book)
                      .FirstOrDefault(b => b.UserId == userId && b.BorrowId == borrowId);
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
}
