using Dll.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PllDoctor.Models;

namespace PllDoctor.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,
                                ILogger<AccountController> logger,
                                SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;

        }


        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new SignUpViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()//to map from database to view model
                {
                    Email = input.Email,
                    //to take username from the email(will be name before @gmail.com)
                    UserName = input.Email.Split("@")[0],
                    Name = input.Name,
                    IsActive = true,
                };

                //Get Password From User And Hashing it with function CreateAsync() which exist in UserManager Class
                var result = await _userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                // Add the user to the "User" role
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    _logger.LogError(error.Description);//appear in consoleApplication
                    ModelState.AddModelError(string.Empty, error.Description);//appear in Ui

                }
            }
            return View(input);
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new SigninViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(SigninViewModel input)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _userManager.FindByEmailAsync(input.Email);

                if (userEmail is null)
                    ModelState.AddModelError("", "Email Does not Exist");
                if (userEmail != null && await _userManager.CheckPasswordAsync(userEmail, input.Password))
                {
                    var result = await _signInManager.PasswordSignInAsync(userEmail, input.Password, input.RememberMe, true);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAllSubjects", "Subject");
                    }
                }

            }
            return View(input);
        }
        #endregion

        #region SignOut
        [HttpPost]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User signed out successfully.");
            return RedirectToAction(nameof(Login));
        }
        #endregion

        public IActionResult ForgetPassword()
        {
            return View(new ForgetPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is null)
                    ModelState.AddModelError("", "Email Does Not exist");
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = Url.Action("ResetPassword", "Account", new { user = user.Email, Token = token }, Request.Scheme);


                    var email = new Email
                    {
                        Title = "Reset Password",
                        body = resetLink,
                        To = input.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }


            }
            return View(input);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewModel());
            //using anynmous declare to send eamil and token to the view model  and receive this data with hidden field in the view to send it to ResetPassword [httppost] Action to can use it in ResetPasswordAsync()
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);//to catch email from user
                if (user is null)
                    ModelState.AddModelError("", "Email Does Not exist");
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, input.token, input.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }

                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.Description);
                        ModelState.AddModelError("", error.Description);

                    }
                }
            }
            return View(input);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
