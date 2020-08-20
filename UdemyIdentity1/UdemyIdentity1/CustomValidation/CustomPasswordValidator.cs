using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyIdentity1.Models;

namespace UdemyIdentity1.CustomValidation
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if(password.Contains(user.UserName, StringComparison.OrdinalIgnoreCase))
            {
                if(!user.Email.Contains(user.UserName.ToLower()))
                {
                    errors.Add(new IdentityError() { Code = "PasswordContainsUserName", Description = "şifre alanı kullanıcı adı içeremez" });
                }
            }

            if (password.Contains(user.Email, StringComparison.OrdinalIgnoreCase))
            {
                errors.Add(new IdentityError() { Code = "PasswordContainsEmail", Description = "şifre alanı email içeremez" });
            }

            if (password.Contains("1234", StringComparison.OrdinalIgnoreCase))
            {
                errors.Add(new IdentityError() { Code = "PasswordContains1234", Description = "şifre alanı ardışıl sayı içeremez" });
            }

            if(errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            } 
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
