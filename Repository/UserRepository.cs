using LMSAPI.Data;
using LMSAPI.Models;
using LMSAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LMSAPI.Repository;
//Repository to abastract the LibraryDbContext with User
public class UserRepository : IUserRepository
{
    private readonly LibraryDbContext _context;

    public UserRepository(LibraryDbContext context)
    {
        _context = context;
    }

    /// Retrieves a user by their username
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(user => user.Username == username);
    }

    /// Adds a new user to the database
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

}