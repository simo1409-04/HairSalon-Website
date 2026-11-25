using System;
using HairSalon.Services.Core.Contracts;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace HairSalon.Services.Core.NewFolder
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SendEmail(string toEmail, string mailSubject, string mailBody)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var port = int.Parse(_configuration["EmailSettings:Port"]);
                var fromMail = _configuration["EmailSettings:Username"];
                var password = _configuration["EmailSettings:Password"];

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Graphite Barber Studio",fromMail));
                email.To.Add(new MailboxAddress("", toEmail));
                email.Subject = mailSubject;
                email.Body = new TextPart("plain") { Text = mailBody };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(fromMail, password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return "Email sent successfully!";
            }


            catch (Exception ex)
            {
                // Log the exception (not done here for simplicity)
                return $"Email sending failed. Error: {ex.Message}";
            }
        }
    }
}

