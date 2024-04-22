﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Terminal;
using Vehicle_Rent.Data;
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
        public async Task<IActionResult> Register(RegisterVM registervm)
        {
            if (!ModelState.IsValid)
            {

                return View(registervm);
            }
            else
            {

                if (registervm.ImageUrl != null && registervm.ImageUrl.Length > 0)
                {
                    // Récupérer le nom du fichier
                    var fileName = Path.GetFileName(registervm.ImageUrl.FileName);
                    // Sauvegarder l'image dans un répertoire sur le serveur
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await registervm.ImageUrl.CopyToAsync(stream);
                    }
                    // Assigner le chemin de l'image à une propriété du modèle ou sauvegarder le chemin dans la base de données
                }
                var user = await _userManager.FindByEmailAsync(registervm.Email);
                if (user != null)
                {
                    ViewBag.ErrorMessage = "This Email Address has already been taken!";
                    return View(registervm);
                }
                else
                {
                    if (registervm.Password != registervm.ConfirmationPassword)
                    {
                        ViewBag.ErrorMessage = "Password and Confirmation Password are not the same!";
                        return View(registervm);
                    }
                    else
                    {
                        var newUser = new User()
                        {
                            Name = registervm.Name,
                            UserName = registervm.Email,
                            Email = registervm.Email
                        };
                        var newUserResponse = await _userManager.CreateAsync(newUser, registervm.Password);
                        if (newUserResponse.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, UserRoles.Customer);
                            var result = await _signInManager.PasswordSignInAsync(newUser, registervm.Password, registervm.RememberMe, false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "There has been an error, please try again!";
                                return View(registervm);
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Could not create the new user";
                            return View(registervm);
                        }
                    }
                }
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
