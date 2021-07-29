using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;

namespace ToDoList.RestfulAPI.Services
{
    public class SendEmailService
    {
        private readonly IConfiguration _config;

        public SendEmailService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public static async Task Send(ForgotPasswordEmailDto forgotPasswordEmail, string resetPasswordUrl)
        {
            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25,
                Credentials = new NetworkCredential("smtpedgaras@gmail.com", "testas1234"),
            });

            Email.DefaultSender = sender;

            var formattedHtml = $"<h2>Do you want to reset your password?</h2>" +
                $"<p>A request has been received to change the password for your {forgotPasswordEmail.EmailAddress} account. Please click link below to reset your password</p>" +
                $"<p>{resetPasswordUrl}</p>";

            var email = await Email
                .From("smtpedgaras@gmail.com")
                .To("baroniunas@gmail.com")
                .Subject("Reset password link")
                .UsingTemplate(formattedHtml, true)
                .SendAsync();
        }
        public void kazkas()
        {
            _config.GetValue<string>("MyKey");
        }
    }
}
