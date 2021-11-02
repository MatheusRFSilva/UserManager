using System.Collections.Generic;
using System.Linq;
using UserManager.Models;

namespace UserManager.Repositories
{

    public class UserRepository
    {

        public static User GetUser(string username, string password)
        {

            var users = new List<User>();
            users.Add(new User { Name = "Batman", Username = "batman", Password = "batman", Role = "manager" });
            users.Add(new User { Name = "Robin", Username = "robin", Password = "robin", Role = "employee" });

            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }

    }
}