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
    public class BackgroundTasks : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundTasks> logger;
        private NonProfitContext context;
        private Timer timer;
        private EmailManager emailManager;

        public  BackgroundTasks(ILogger<BackgroundTasks> logger, NonProfitContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        //Cancels newsletter subscription
        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            emailManager = new EmailManager(context);
            // timer = new Timer(o => emailManager.SendNewsletter(), null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }
        //Cancels newsletter subscription
        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Weekly email has stopped working");
            return Task.CompletedTask;
        }
        //Send email feature
    }
}
