using System;

namespace UserManager.Models
{

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
    }
}