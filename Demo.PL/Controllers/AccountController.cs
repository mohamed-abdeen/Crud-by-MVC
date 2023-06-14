using Demo.DAL.Entity;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<APPUser> _userManager;
        private readonly SignInManager<APPUser> _signInManager;

        public AccountController(UserManager<APPUser> userManager, SignInManager<APPUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // server side validation
            {
                var user = new APPUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }

            return View(model);
        }
        #endregion

        #region login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Email is not Correct");

                }
                ModelState.AddModelError(string.Empty, "Email is not Exist");

            }

            return View(model);
        }
        #endregion

        #region Sign Out
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(FOrgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user); //token is valid for only one time for this user
                    var resetpasswordlink = Url.Action("RestePassword", "Account", new {  Email = model.Email ,Token=token}, Request.Scheme);
                    var email = new Email()
                    {
                      Subject="Reset Your Subject",
                      To=model.Email,
                      Body= resetpasswordlink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYolurInbox));
                }
                ModelState.AddModelError(string.Empty, "Email Is not Exists");
            }
            return View(model);

        }
        public IActionResult CheckYolurInbox()
        {
            return View();

        }
        #endregion

        #region Reset Password
        public IActionResult RestePassword(string Email ,string Token)
        {
            TempData["Email"] = Email;
            TempData["Token"] = Token;

            return View();
        }
        [HttpPost]
        public async Task< IActionResult> RestePassword(RestePasswordViewModel model )
        {
            if (ModelState.IsValid)
            {
                string Email = TempData["Email"] as string;
                string Token = TempData["Token"] as string;

                var user= await _userManager.FindByEmailAsync(Email);   

                var result= await _userManager.ResetPasswordAsync(user ,Token ,model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        #endregion

    }
}
