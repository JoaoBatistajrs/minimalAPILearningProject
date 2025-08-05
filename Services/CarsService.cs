using AutoMapper;
using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Interfaces.Repository;
using MinimalAPI.Domain.Interfaces.Service;
using MinimalAPI.Domain.Models;
using System.Linq.Expressions;

namespace MinimalAPI.Services;

public class CarsService : ICarsService
{

    private readonly IEntityRepository<Car> _repository;
    private readonly IMapper _mapper;

    public CarsService(IEntityRepository<Car> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Car>? Create(CarModel carModel)
    {
        var car = _mapper.Map<Car>(carModel);
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
