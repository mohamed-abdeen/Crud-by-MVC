﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RestePasswordViewModel
	{
        [Required(ErrorMessage = "password is required")]
        [MinLength(5, ErrorMessage = "minium password length is 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Confirm password does not match password")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

    }
}
