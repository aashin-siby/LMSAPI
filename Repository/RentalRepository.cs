using LMSAPI.Models;
using LMSAPI.Data;
using Microsoft.EntityFrameworkCore;
using LMSAPI.Repository.IRepository;

namespace LMSAPI.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly LibraryDbContext _context;

        public RentalRepository(LibraryDbContext context)
        {
            _context = context;
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


    }
}

