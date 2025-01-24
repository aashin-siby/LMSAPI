using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;

public interface IBorrowRepository
{
     Task BorrowBookAsync(BorrowDetails borrowDetails);
     Task<BorrowDetails> ReturnBookAsync(int borrowId, DateTime returnDate);
}
