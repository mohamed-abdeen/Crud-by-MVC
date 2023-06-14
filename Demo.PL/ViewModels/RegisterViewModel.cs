using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="Email is reqired")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }
		[Required(ErrorMessage ="password is required")]
		[MinLength(5 ,ErrorMessage ="minium password length is 5")]
		[DataType(DataType.Password)]	
		public string Password { get; set; }
		[Required(ErrorMessage ="confirm password is required")]
		[Compare("Password",ErrorMessage ="Confirm password does not match password")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }

	}
}
