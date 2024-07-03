using System.ComponentModel.DataAnnotations;

namespace PllDoctor.Models
{
	public class ResetPasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		[MaxLength(6, ErrorMessage = "Password Must be 6 characters")]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password Miss Match")]
		public string ConfirmPassword { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public string token { get; set; }
	}
}
