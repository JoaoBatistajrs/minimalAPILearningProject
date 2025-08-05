using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Models;

namespace MinimalAPI.Domain.Interfaces.Service
{
    public interface IUserService
    {
        Task<User> CreateUser(UserModel userModel);
        Task<User> GetUserByEmail(string email);
    }
}
