using MinimalAPI.Domain.Entities;

namespace MinimalAPI.Domain.Interfaces.Service
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
