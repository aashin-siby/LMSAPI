using System;
using System.Threading.Tasks;
using LMSAPI.Data;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LMSAPI.Repository;

    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task BorrowBookAsync(BorrowDetails borrowDetails)
        {
            _context.BorrowDetails.Add(borrowDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<BorrowDetails> ReturnBookAsync(int borrowId, DateTime returnDate)
        {
            var borrowDetails = await _context.BorrowDetails.FindAsync(borrowId);
            if (borrowDetails == null)
            {
                throw new Exception("Borrow record not found");
            }

            borrowDetails.ReturnDate = returnDate;

            var daysBorrowed = (returnDate - borrowDetails.BorrowDate).Days;
            if (daysBorrowed > 10)
            {
                borrowDetails.Penalty = 100 + (daysBorrowed - 10) * 5;
            }

            _context.BorrowDetails.Update(borrowDetails);
            await _context.SaveChangesAsync();

            return borrowDetails;
        }
    }
