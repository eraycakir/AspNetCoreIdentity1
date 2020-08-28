using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyIdentity1.Models;
using UdemyIdentity1.Models.ViewModels;

namespace UdemyIdentity1.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public MemberController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IActionResult Index()
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();
            return View(userViewModel);
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                bool exist = _userManager.CheckPasswordAsync(user, passwordChangeViewModel.PasswordOld).Result;
                if (exist)
                {
                    IdentityResult result = _userManager.ChangePasswordAsync(user, passwordChangeViewModel.PasswordOld, passwordChangeViewModel.PasswordNew).Result;
                    if (result.Succeeded)
                    {
                        _userManager.UpdateSecurityStampAsync(user);

                        // BackEnd 'te çıkış ve tekrar giriş.
                        _signInManager.SignOutAsync();
                        _signInManager.PasswordSignInAsync(user, passwordChangeViewModel.PasswordNew, true, false);
                        // _signInManager.SignInAsync()

                        ViewBag.Success = true;
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eski şifreniz yanlış.");
                }
            }
            return View(passwordChangeViewModel);
        }
    }
}
