using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Services
{
    public class SendEmailService
    {
        public static async Task Send(ForgotPasswordEmail forgotPasswordEmail)
        {
            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25,
                Credentials = new NetworkCredential("smtpedgaras@gmail.com", "testas1234"),
            });

            Email.DefaultSender = sender;

            var formattedHtml = $"<h2>Hello user,</h2>" +
                $"<p>A request has been received to change the password for your {forgotPasswordEmail.EmailAddress} account. Please click link below to reset your password</p>" +
                $"<p>https://localhost:44304/login/reset-password/{forgotPasswordEmail.EmailAddress}</p>";

            var email = await Email
                .From("smtpedgaras@gmail.com")
                .To("baroniunas@gmail.com")
                .Subject("Reset password link")
                .UsingTemplate(formattedHtml, true)
                .SendAsync();
        }
    }
}
