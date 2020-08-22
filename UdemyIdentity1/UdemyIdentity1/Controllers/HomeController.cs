using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentity1.Models;
using UdemyIdentity1.Models.ViewModels;

namespace UdemyIdentity1.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public HomeController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser();
                appUser.UserName = userViewModel.UserName;
                appUser.Email = userViewModel.Email;
                appUser.PhoneNumber = userViewModel.PhoneNumber;

                IdentityResult result = await _userManager.CreateAsync(appUser, userViewModel.Password); // _userManager.CreateAsync(appUser, userViewModel.Password).Result;

                if (result.Succeeded)
                {
                    // Oto. Login yapılabilir. 
                    // ama Login sayfasına yönlendirip kullanıcının girdiği kullanıcı adı ve şifreyi tekrar girdirmek daha yaygın bir yöntem.
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (IdentityError identityError in result.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
            return View(userViewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(userLogin.Email);

                if(user != null)
                {
                    await _signInManager.SignOutAsync(); // Önce SignOut yapalım.

                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, 
                        userLogin.Password,isPersistent: true, lockoutOnFailure: false); 
                    
                   if(signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz email adresi veya şifresi");
                }
            }

            return View();
        }
    }
}
