using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NonProfitProject.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace NonProfitProject.Code
{
    public class BackgroundWeeklyEmail : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundWeeklyEmail> logger;
        private readonly NonProfitContext context;
        private Timer timer;

        public  BackgroundWeeklyEmail(ILogger<BackgroundWeeklyEmail> logger, NonProfitContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(o => SendEmail(), null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Weekly email has stopped working");
            return Task.CompletedTask;
        }
        private void SendEmail()
        {
            List<User> users = new List<User>();
            try
            {
                users = context.Users.Where(x => x.recieveWeeklyNewsletter == true).ToList();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bankd Tech Solutions Community Team", "BankdTechSolutions@gmail.com"));
            message.Subject = "Weekly Newsletter";
            message.Body = new TextPart("plain")
            {
                Text = "This is a test newsletter"
            };
            foreach(User u in users){
                message.To.Add(new MailboxAddress(u.UserFirstName, u.Email));
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("BankdTechSolutions@gmail.com", "987963Gizm0");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
        }
    }
}
