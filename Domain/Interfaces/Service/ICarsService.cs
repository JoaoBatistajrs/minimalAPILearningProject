using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Models;

namespace MinimalAPI.Domain.Interfaces.Service;

public interface ICarsService
{
    Task<Car>? Create(CarModel car);
    Task<PagedResult<Car>> GetCarsAsync(string? make, string? model, int page, int pageSize);
    Task<Car>? GetById(int id);
    
}
