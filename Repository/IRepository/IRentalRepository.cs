using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;

//Interface which will helps to give the rental details of the particular user 
public interface IRentalRepository
{

     /// Retrieves all rental details for a specific user.
     IEnumerable<BorrowDetails> GetUserRentals(int userId);
}


