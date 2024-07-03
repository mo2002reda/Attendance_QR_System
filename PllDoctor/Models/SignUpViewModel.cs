using System.ComponentModel.DataAnnotations;

namespace PllDoctor.Models
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "Email is Required")]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"^\w+\d+@gmail\.com$", ErrorMessage = "Please Enter Valid Gmail")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Your Name Is Required")]
		public string Name { get; set; }
		[Required]
		[MaxLength(6, ErrorMessage = "Password Must Contain UpperCase & LowerCase & Digits")]
		public string Password { get; set; }
		[Required]
		[MaxLength(6)]
		[Compare("Password", ErrorMessage = "Miss match with Password")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "Accept For Terms")]
		public bool IsActive { get; set; }


	}
}
