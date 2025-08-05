using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Models;

namespace MinimalAPI.Domain.Interfaces.Repository
{
    public interface ICarsRepository
    {
        Task<Car?> UpdateAsync(int id, CarModel updatedModel);
    }
}
