using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;

//Repository on all the methods for the User
public interface IUserRepository
{
    
    // Retrieves a user by their username 
    Task<User> GetUserByUsernameAsync(string username);

    // Adds a new user to the database asynchronously.
    Task AddUserAsync(User user);
}