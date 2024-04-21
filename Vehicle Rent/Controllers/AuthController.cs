using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.AuthVM;

namespace Vehicle_Rent.Controllers
{
    public class AuthController : Controller 
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;   

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            var loginVM = new LoginVM();
            return View(loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Account not found.";
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid email or password.";
            return View(loginViewModel);
        }



        public IActionResult Register()
        {
            var registerVM = new RegisterVM();
            return View(registerVM);
        }
 
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

            var newUser = new User { UserName = registerViewModel.Email, Email = registerViewModel.Email, Name = registerViewModel.Name };
            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
