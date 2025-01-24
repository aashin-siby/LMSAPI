using LMSAPI.Models;

namespace LMSAPI.Repository.IRepository;
public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task AddUserAsync(User user);
}