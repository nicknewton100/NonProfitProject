using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NonProfitProject.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Models.Settings;
using Microsoft.Extensions.Options;

namespace NonProfitProject.Code
{
    public class EmailManager : IEmailManager
    {
        private NonProfitContext context;
        private readonly MailSettings _options;

        public EmailManager(NonProfitContext context, IOptions<MailSettings> options)
        {
            _options = options.Value;
            this.context = context;
        }
        //used to send weekly news letter to users however this was cut due to lack of time
        public async Task SendNewsletter()
        {
            List<User> users = new List<User>();
            try
            {
                users = await context.Users.Where(x => x.ReceiveWeeklyNewsletter == true).ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            MimeMessage message = CreateSimpleMessage("Weekly Newsletter", "This is the newsletter for the week!");

            await SendEmail(users, message);
        }
        //sends email to a user
        public async Task SendEmail(User user, MimeMessage message)
        {
            message.To.Add(new MailboxAddress(user.UserFirstName, user.Email));
            using (var client = new SmtpClient())
            {
                //connect to email server
                client.Connect(_options.Host, _options.Port, false);
                client.Authenticate(_options.Mail, _options.Password);
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
        //sends a email to a email address
        public async Task SendEmail(string email, MimeMessage message)
        {
            message.To.Add(new MailboxAddress(email));
            using (var client = new SmtpClient())
            {
                //connect to email server
                await client.ConnectAsync(_options.Host, _options.Port, false);
                await client.AuthenticateAsync(_options.Mail, _options.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        //sends email to multiple users
        public async Task SendEmail(List<User> users, MimeMessage message)
        {
            foreach(User u in users)
            {
                await SendEmail(u, message);
            }
        }
        //creates simple, plain text messages
        public MimeMessage CreateSimpleMessage(string subject, string bodyText)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.DisplayName, _options.Mail));
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
            message.From.Add(new MailboxAddress(_options.DisplayName, _options.Mail));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = html
            };
            return message;
        }

    }
}
