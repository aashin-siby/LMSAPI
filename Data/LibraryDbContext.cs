using LMSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSAPI.Data
{
    // Class which interact with the database using EF Core ORM
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowDetails> BorrowDetails { get; set; }

    }
}
