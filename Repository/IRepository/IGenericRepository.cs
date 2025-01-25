
namespace LMSAPI.Repository.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAll(int page, int pageSize);
    
}