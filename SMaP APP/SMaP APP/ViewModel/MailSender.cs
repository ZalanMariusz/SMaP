using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.ViewModel
{
    static class MailSender
    {
        public static void SendMail(string userMailAddress)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = new NetworkCredential(MailResources.MailUserName, MailResources.MailPassword)
            };
            userMailAddress = MailResources.IsRelease == "false" ? MailResources.MailUserName : userMailAddress;
            MailMessage mail = new MailMessage(MailResources.MailUserName, userMailAddress, "SMaP regisztráció", "Ön regisztrálva lett az SMaP rendszerbe. Az alapértelmezett belépési azonosítója és jelszava az email címe.A jelszavát első belépéskor meg kell változtatnia!");
            client.SendAsync(mail, null);
        }
        
    }
}
