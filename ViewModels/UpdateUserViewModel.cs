using System.ComponentModel.DataAnnotations;

namespace UserManager.ViewModels
{

    public class UpdateUserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

    }
}