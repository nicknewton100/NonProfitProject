using MimeKit;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Code
{
    public interface IEmailManager
    {
        Task SendNewsletter();
        Task SendEmail(User user, MimeMessage message);
        Task SendEmail(string email, MimeMessage message);
        Task SendEmail(List<User> users, MimeMessage message);
        MimeMessage CreateSimpleMessage(string subject, string bodyText);
        MimeMessage CreateHTMLMessage(string subject, string html);
    }
}
