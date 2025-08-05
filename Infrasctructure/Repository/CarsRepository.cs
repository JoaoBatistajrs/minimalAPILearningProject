using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Interfaces.Repository;
using MinimalAPI.Domain.Models;
using MinimalAPI.Infrasctructure.DataBase;

namespace MinimalAPI.Infrasctructure.Repository;

public class CarsRepository : ICarsRepository
{
    private readonly CarsContext _context;

    public CarsRepository(CarsContext context)
    {
        _context = context;
    }
    public async Task<Car?> UpdateAsync(int id, CarModel updatedModel)
    {
        var existingCar = await _context.Cars.FindAsync(id);

        if (existingCar == null)
            return null;

        existingCar.Make = updatedModel.Make;
        existingCar.Model = updatedModel.Model;
        existingCar.Year = updatedModel.Year;

        _context.Cars.Update(existingCar);
        await _context.SaveChangesAsync();

        return existingCar;
    }
}
