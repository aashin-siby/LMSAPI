using LMSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSAPI.Data
{
    // Class which interact with the database using EF Core ORM
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        //User model to db
        public DbSet<User> Users { get; set; }
        //Book model to db
        public DbSet<Book> Books { get; set; }
        //BorrowDetails model to db
        public DbSet<BorrowDetails> BorrowDetails { get; set; }

    }
}
