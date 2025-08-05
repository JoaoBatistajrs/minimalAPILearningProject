using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Interfaces.Repository;
using MinimalAPI.Domain.Interfaces.Service;
using MinimalAPI.Domain.Models;

namespace MinimalAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IEntityRepository<User> _repository;
        private readonly IPasswordService _passwordService;

        public UserService(IEntityRepository<User> repository, IPasswordService passwordService)
        {
            _repository = repository;
            _passwordService = passwordService;
        }

        public async Task<User> CreateUser(UserModel userModel)
        {
            var user = new User
            {
                Name = userModel.Name,
                Email = userModel.Email,
                CreatedAt = DateTime.UtcNow,    
                UpdatedAt = DateTime.UtcNow
            };

            user.Password = _passwordService.HashPassword(user, userModel.Password);

            return await _repository.CreateAsync(user);
        }
    }
}
