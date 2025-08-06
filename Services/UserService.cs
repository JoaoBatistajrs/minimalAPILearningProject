using MinimalAPI.Domain.Entities;
using MinimalAPI.Domain.Enums;
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
                Role = UserRole.User
            };

            user.Password = _passwordService.HashPassword(user, userModel.Password);

            return await _repository.CreateAsync(user);
        }

        public async Task<User> CreateAdminUser(AdminCreateUserModel adminUserModel)
        {
            var user = new User
            {
                Name = adminUserModel.Name,
                Email = adminUserModel.Email,
                Role = UserRole.Adm
            };

            user.Password = _passwordService.HashPassword(user, adminUserModel.Password);

            return await _repository.CreateAsync(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _repository.GetPagedAsync(u => u.Email == email, 1, 1)
                .ContinueWith(task => task.Result.Items.FirstOrDefault());

            return result ?? throw new KeyNotFoundException($"User with email {email} not found.");
        }
    }
}
