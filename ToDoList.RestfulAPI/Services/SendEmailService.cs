using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;

namespace ToDoList.RestfulAPI.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _config;
        private readonly string _senderEmail;
        private readonly string _password;
        private readonly string _smtpServerHost;
        private readonly int _smtpServerPort;
        private readonly string _emailToSend;

        public SendEmailService(IConfiguration configuration)
        {
            _config = configuration;
            _senderEmail = _config.GetSection("Credentials").GetSection("SenderEmail").Value;
            _password = _config.GetSection("Credentials").GetSection("Password").Value;
            _smtpServerHost = _config.GetValue<string>("SmtpServerHost");
            _smtpServerPort = int.Parse(_config.GetValue<string>("SmtpServerPort"));
        }

        public async Task Send(ForgotPasswordEmailDto forgotPasswordEmail, string resetPasswordUrl)
        {
            var sender = new SmtpSender(() => new SmtpClient(_smtpServerHost, _smtpServerPort)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_senderEmail, _password),
            });

            Email.DefaultSender = sender;

            var formattedHtml = $"<h2>Do you want to reset your password?</h2>" +
                $"<p>A request has been received to change the password for your {forgotPasswordEmail.EmailAddress} account. Please click link below to reset your password</p>" +
                $"<p>{resetPasswordUrl}</p>";

            var email = await Email
                .From(_senderEmail, "EdgaroSMTP")
                .To(forgotPasswordEmail.EmailAddress)
                .Subject("Reset password link")
                .UsingTemplate(formattedHtml, true)
                .SendAsync();
        }
    }
}
