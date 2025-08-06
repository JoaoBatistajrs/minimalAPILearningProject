using MinimalAPI.Domain.Entities;
using System.Linq.Expressions;

namespace MinimalAPI.Domain.Interfaces.Repository
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>>? filter, int pageNumber, int pageSize);
    }
}
