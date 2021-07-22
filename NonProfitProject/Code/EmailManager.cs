using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NonProfitProject.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;

namespace NonProfitProject.Code
{
    public class EmailManager
    {
        private NonProfitContext context;
        private readonly string FromEmail = "BankdTechSolutions@gmail.com";
        private readonly string password = "987963Gizm0";
        private readonly string FromName = "Bankd Tech Solutions Community Team";
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        public EmailManager(NonProfitContext context)
        {
            this.context = context;
        }

        public void SendNewsletter()
        {
            List<User> users = new List<User>();
            try
            {
                users = context.Users.Where(x => x.ReceiveWeeklyNewsletter == true).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            MimeMessage message = CreateSimpleMessage("Weekly Newsletter", "This is the newsletter for the week!");

            SendEmail(users, message);
        }
        public void SendEmail(User user, MimeMessage message)
        {
            message.To.Add(new MailboxAddress(user.UserFirstName, user.Email));
            using (var client = new SmtpClient())
            {
                //connect to email server
                client.Connect(smtpServer, smtpPort, false);
                client.Authenticate(FromEmail, password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        public void SendEmail(List<User> users, MimeMessage message)
        {
            foreach(User u in users)
            {
                SendEmail(u, message);
            }
        }
        public MimeMessage CreateSimpleMessage(string subject, string bodyText)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(FromName, FromEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = bodyText
            };
            return message;
        }

    }
}
