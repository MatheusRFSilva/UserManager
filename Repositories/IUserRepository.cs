using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.Models;
using UserManager.ViewModels;

namespace UserManager.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();

        Task<User> GetUserById(Guid id);

        Task<User> CreateUSer(User user);
        Task UpdateUser(User user, UpdateUserViewModel model);

        Task DeleteUser(User user);

        Task<User> Login(string username, string password);

    }
}