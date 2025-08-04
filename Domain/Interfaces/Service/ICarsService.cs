using MinimalAPI.Domain.Entities;

namespace MinimalAPI.Domain.Interfaces.Service
{
    public interface ICarsService
    {
        Task<Car>? Create(Car car);
        Task<PagedResult<Car>> GetCarsAsync(string? make, string? model, int page, int pageSize);
        Task<Car>? GetById(int id);
        
    }
}
