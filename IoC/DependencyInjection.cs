using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Interfaces.Repository;
using MinimalAPI.Domain.Interfaces.Service;
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

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICarsService, CarsService>();

        return services;
    }
}
