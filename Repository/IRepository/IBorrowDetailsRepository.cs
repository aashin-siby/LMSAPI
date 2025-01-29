using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;
// Repository interface defining all methods for managing BorrowDetails
public interface IBorrowDetailsRepository

{

     /// Adds a new borrow record to the database.
     void AddBorrowDetails(BorrowDetails borrowDetails);

     /// Retrieves all borrow details for a specific user.
     IEnumerable<BorrowDetails> GetBorrowDetailsByUserId(int userId);

     /// Retrieves a specific borrow record for a user based on the borrow ID.
     BorrowDetails GetBorrowDetailsByUserIdAndBookId(int userId, int borrowId);

     /// Updates an existing borrow record in the database.
     void UpdateBorrowDetails(BorrowDetails borrowDetails);

     /// Saves all changes made to the database context.
     void Save();
}