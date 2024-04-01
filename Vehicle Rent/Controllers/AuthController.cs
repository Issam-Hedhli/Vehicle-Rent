using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.Auth;

namespace Vehicle_Rent.Controllers
{
    public class AuthController : Controller 
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;   

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");  
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(loginViewModel);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("-------------------Validation error------------------");
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "This email address is already registered.");
                return View(registerViewModel);
            }

            if (registerViewModel.Password != registerViewModel.ConfirmationPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(registerViewModel);
            }

            var newUser = new User { UserName = registerViewModel.Email, Email = registerViewModel.Email };
            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { message = "Registration successful!" });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerViewModel); 
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
