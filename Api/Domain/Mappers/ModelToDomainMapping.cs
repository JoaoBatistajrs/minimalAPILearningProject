using AutoMapper;
using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Models;

namespace MinimalAPI.Domain.Mappers;

public class ModelToDomainMapping : Profile
{
    public ModelToDomainMapping()
    {
        CreateMap<CarModel, Car>();
    }
}