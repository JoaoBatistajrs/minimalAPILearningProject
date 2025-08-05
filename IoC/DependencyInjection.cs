using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Interfaces.Repository;
using MinimalAPI.Domain.Interfaces.Service;
using MinimalAPI.Domain.Mappers;
using MinimalAPI.Domain.Models;
using MinimalAPI.Helper;
using MinimalAPI.Infrasctructure.DataBase;
using MinimalAPI.Infrasctructure.Repository;
using MinimalAPI.Services;

namespace MinimalAPI.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStrucuture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarsContext>(options => options.UseNpgsql(configuration.GetConnectionString("CarsApiConnectionString")));

        services.AddScoped<IEntityRepository<Car>, EntityRepository<Car>>();
        services.AddScoped<IEntityRepository<User>, EntityRepository<User>>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg => { }, typeof(DomainToModelMapping));
        services.AddScoped<IValidator<CarModel>, CarModelValidator>();
        services.AddScoped<ICarsService, CarsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
