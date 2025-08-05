using AutoMapper;
using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Models;

namespace MinimalAPI.Domain.Mappers;

public class DomainToModelMapping : Profile
{
    public DomainToModelMapping()
    {
        CreateMap<Car, CarModel>().ReverseMap();
    }
}
