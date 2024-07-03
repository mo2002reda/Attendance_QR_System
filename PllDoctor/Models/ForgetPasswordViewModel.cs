using System.ComponentModel.DataAnnotations;

namespace PllDoctor.Models
{
	public class ForgetPasswordViewModel
	{
		[Required]
		[EmailAddress(ErrorMessage = "InValid Email")]
		public string Email { get; set; }
	}
}
