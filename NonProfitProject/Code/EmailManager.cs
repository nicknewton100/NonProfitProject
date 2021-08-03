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
        //used to send weekly news letter to users however this was cut due to lack of time
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
        //sends email to a user
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
        //sends a email to a email address
        public void SendEmail(string email, MimeMessage message)
        {
            message.To.Add(new MailboxAddress(email));
            using (var client = new SmtpClient())
            {
                //connect to email server
                client.Connect(smtpServer, smtpPort, false);
                client.Authenticate(FromEmail, password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        //sends email to multiple users
        public void SendEmail(List<User> users, MimeMessage message)
        {
            foreach(User u in users)
            {
                SendEmail(u, message);
            }
        }
        //creates simple, plain text messages
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
        //creates a message that will be rendered in HTML
        public MimeMessage CreateHTMLMessage(string subject, string html)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(FromName, FromEmail));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = html
            };
            return message;
        }

    }
}
