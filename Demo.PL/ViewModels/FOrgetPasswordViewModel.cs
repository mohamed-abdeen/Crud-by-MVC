using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class FOrgetPasswordViewModel
	{

        [Required(ErrorMessage = "Email is reqired")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
