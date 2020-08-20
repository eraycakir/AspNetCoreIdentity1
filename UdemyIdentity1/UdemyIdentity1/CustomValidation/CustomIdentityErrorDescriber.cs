using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyIdentity1.CustomValidation
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName)
        {
            // return base.InvalidUserName(userName);
            return new IdentityError()
            {
                Code = "InvalidUserName",
                Description = $"Bu {userName} geçersizdir."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"Bu kullanıcı adı {userName} zaten kullanılmaktadır."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            //return base.DuplicateEmail(email);
            return new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = $"Bu email {email} kullanılmaktadır."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            //return base.PasswordTooShort(length);
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Şifreniz en az {length} karakter olmalıdır."
            };
        }




    }
}
