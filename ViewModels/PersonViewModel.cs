using System.ComponentModel.DataAnnotations;

namespace Sample.AspNetCoreWebApi.ViewModels
{
    public class PersonViewModel
    {

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
    }
}