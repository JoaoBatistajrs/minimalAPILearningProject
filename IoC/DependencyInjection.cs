using Microsoft.EntityFrameworkCore;
using MinimalAPI.Infrasctructure.DataBase;

namespace MinimalAPI.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStrucuture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarsContext>(options => options.UseNpgsql(configuration.GetConnectionString("CarsApiConnectionString")));

        return services;
    }
}
