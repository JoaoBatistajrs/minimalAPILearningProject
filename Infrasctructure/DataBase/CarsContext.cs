using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain.Entities;

namespace MinimalAPI.Infrasctructure.DataBase;

public class CarsContext : DbContext
{

    public CarsContext(DbContextOptions<CarsContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Car> Cars { get; set; } = null!;
}
