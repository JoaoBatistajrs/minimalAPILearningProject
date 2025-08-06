using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Interfaces.Repository;
using MinimalAPI.Infrasctructure.DataBase;
using System.Linq.Expressions;

namespace MinimalAPI.Infrasctructure.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly CarsContext _context;

        public EntityRepository(CarsContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>>? filter, int pageNumber, int pageSize)
        {
            var query = _context.Set<T>().AsQueryable();

            if (filter is not null)
                query = query.Where(filter);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}