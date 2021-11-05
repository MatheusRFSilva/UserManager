using Microsoft.EntityFrameworkCore;
using UserManager.Models;

namespace UserManager.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }


    }


}