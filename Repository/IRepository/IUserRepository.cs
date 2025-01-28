using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;
//Repository on all the methods for the User
public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task AddUserAsync(User user);
}