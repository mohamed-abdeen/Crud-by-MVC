using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Email is reqired")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is required")]
        [MinLength(5, ErrorMessage = "minium password length is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
