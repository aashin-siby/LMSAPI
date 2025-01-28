using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;
//Repository on all the methods for the BorrowDetails
public interface IBorrowDetailsRepository
{
     void AddBorrowDetails(BorrowDetails borrowDetails);
     IEnumerable<BorrowDetails> GetBorrowDetailsByUserId(int userId);
     BorrowDetails GetBorrowDetailsByUserIdAndBookId(int userId, int bookId);
     void UpdateBorrowDetails(BorrowDetails borrowDetails);
     void Save();
}