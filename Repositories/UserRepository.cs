using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.Models;
using UserManager.ViewModels;

namespace UserManager.Repositories
{

    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUSer(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context
            .Users
            .AsNoTracking()
            .ToListAsync();

            return users;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            return user;
        }

        public async Task UpdateUser(User user, UpdateUserViewModel model)
        {
            user.Name = model.Name;
            user.Password = model.Password;
            user.Role = model.Role;

            await _context
            .SaveChangesAsync();

        }
    }
}