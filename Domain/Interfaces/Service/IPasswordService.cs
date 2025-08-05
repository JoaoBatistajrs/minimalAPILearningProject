using MinimalAPI.Domain.Entities;

namespace MinimalAPI.Domain.Interfaces.Service
{
    public interface IPasswordService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string password);
    }
}
