
// using LMSAPI.Data;
// using LMSAPI.Repository.IRepository;
// using Microsoft.EntityFrameworkCore;

// namespace LMSAPI.Repository;

// public class GenericRepository<T> : IGenericRepository<T> where T : class
// {
//      private readonly LibraryDbContext _dbContext;

//              public GenericRepository(LibraryDbContext dbContext)
//         {
//             _dbContext = dbContext;
//         }
        
//          public async Task<List<T>> GetALL(int page,int pageSize)
//         {
            
//             return await _dbContext.Set<T>()
//                         .Skip((page -1 )* pageSize)
//                         .Take(pageSize)
//                         .ToListAsync();
//         }
// }