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


    public void AddBorrowDetails(BorrowDetails borrowDetails)
    {
        _context.BorrowDetails.Add(borrowDetails);
    }

    public IEnumerable<BorrowDetails> GetBorrowDetailsByUserId(int userId)
    {
        return _context.BorrowDetails
                       .Include(b => b.Book)
                       .Where(b => b.UserId == userId)
                       .ToList();
    }

    public BorrowDetails GetBorrowDetailsByUserIdAndBookId(int userId, int bookId)
    {
         return _context.BorrowDetails
                       .Include(b => b.Book)
                       .SingleOrDefault(b => b.UserId == userId && b.BookId == bookId);
    }

    public void UpdateBorrowDetails(BorrowDetails borrowDetails)
    {
        _context.BorrowDetails.Update(borrowDetails);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
