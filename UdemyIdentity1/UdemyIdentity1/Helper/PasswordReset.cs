using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace UdemyIdentity1.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link)
        {
            //MailMessage mail = new MailMessage();

            //SmtpClient smtpClient = new SmtpClient("mail.teknohub.net");



            string body = "<head>" +
            "Here comes some logo" +
        "</head>" +
        "<body>" +
            "<h1>Account confirmation reqest.</h1>" + Environment.NewLine +
            "<a>Dear User, </a>" + Environment.NewLine +
            "<a>In order to be able to use musicshop app properly, we require You to confirm Your email address.</a>" + Environment.NewLine +
            "<a>This is the last step towards using our app.</a>" + Environment.NewLine +
            "<a>Pleas follow this hyperlink to confirm your address.</a>" + Environment.NewLine +
            "<a href=" + link + "> şifre yenilenem linki</a>" +
        "</body>";
            try
            {
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential()
                    {
                        UserName = "mehmetyagci53@gmail.com", // Config.Username
                        Password = "pnkgdyewylbwwnrk", // Config.Password
                    };
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;

                    //Oops: from/recipients switched around here...
                    //smtpClient.Send("targetemail@targetdomain.xyz", "myemail@gmail.com", "Account verification", body);
                    smtpClient.Send("mehmetyagci53@gmail.com", "mehmetyagci53@hotmail.com", "Account verification", body);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("{0}: {1}", e.ToString(), e.Message);
            }

        }
    }
}
