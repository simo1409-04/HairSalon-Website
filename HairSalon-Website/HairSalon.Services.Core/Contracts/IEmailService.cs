using System;
using System.Collections.Generic;
using System.Text;

namespace HairSalon.Services.Core.Contracts
{
    public interface IEmailService
    {
        Task<string> SendEmail(string toEmail, string subject, string body);
    }
}
