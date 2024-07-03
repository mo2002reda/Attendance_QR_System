using System.ComponentModel.DataAnnotations;
namespace PllDoctor.Models
{
    public class SigninViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        [MaxLength(6)]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }

    }

}

