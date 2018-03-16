using System.ComponentModel.DataAnnotations;

namespace Sample.AspNetCoreWebApi.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}