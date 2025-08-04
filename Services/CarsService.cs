using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Interfaces.Repository;
using MinimalAPI.Domain.Interfaces.Service;
using System.Linq.Expressions;

namespace MinimalAPI.Services
{
    public class CarsService : ICarsService
    {

        private readonly IEntityRepository<Car> _repository;

        public CarsService(IEntityRepository<Car> repository)
        {
            _repository = repository;
        }

        public async Task<Car>? Create(Car car)
        {
            return await _repository.CreateAsync(car);
        }

        public async Task<PagedResult<Car>> GetCarsAsync(string? make, string? model, int page, int pageSize)
        {
            Expression<Func<Car, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(make) || !string.IsNullOrWhiteSpace(model))
            {
                filter = c =>
                    (make == null || c.Make == make) &&
                    (model == null || c.Model == model);
            }

            return await _repository.GetPagedAsync(filter, page, pageSize);
        }

        public async Task<Car>? GetById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
