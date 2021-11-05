using System.ComponentModel.DataAnnotations;

namespace UserManager.ViewModels
{

    public class CreateUserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}