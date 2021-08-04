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
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Code.Security;

namespace NonProfitProject.Code
{
    public class BackgroundTasks : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundTasks> logger;
        private NonProfitContext context;
        private Timer timer;

        public  BackgroundTasks(ILogger<BackgroundTasks> logger, NonProfitContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        //runs the RenewMembership() method every 30 minutes- waits 5 minutes after startup and then waits 30 minutes between each one.
        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(o => RenewMembership(), null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(25));
            return Task.CompletedTask;
        }
        //Cancels renew membership async method
        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Membership renew has stopped working");
            return Task.CompletedTask;
        }
        //checks if the user has a membership that expires on this day and if it does, it will renew it if its active
        public void RenewMembership()
        {
            List<MembershipDues> membershipDues;
            try
            {
                membershipDues = context.MembershipDues.Include(md => md.MembershipType).Include(md => md.Receipt).ThenInclude(r => r.InvoicePayment).Where(md => md.MemActive == true && md.MemRenewalDate.Date <= DateTime.UtcNow.Date).ToList();
                System.Diagnostics.Debug.WriteLine("Success");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Test: " + ex.Message);
                return;
            }
            if(membershipDues.Count() > 0)
            {
                foreach (var due in membershipDues)
                {
                    due.MemActive = false;
                    due.Receipt.Total = due.MembershipType.Amount;
                    context.MembershipDues.Update(due);

                    Receipts receipt = new Receipts()
                    {
                        UserID = due.UserID,
                        Total = due.MembershipType.Amount,
                        Date = DateTime.UtcNow,
                        Description = "Membership Due"
                    };
                    receipt.MembershipDue = new MembershipDues()
                    {
                        UserID = due.UserID,
                        MembershipTypeID = due.MembershipType.MembershipTypeID,
                        MemStartDate = DateTime.UtcNow,
                        MemEndDate = DateTime.UtcNow.AddMonths(1),
                        MemRenewalDate = DateTime.UtcNow.AddMonths(1),
                        MemActive = true,
                    };
                    receipt.InvoicePayment = new InvoicePayment()
                    {
                        CardholderName = due.Receipt.InvoicePayment.CardholderName,
                        CardType = due.Receipt.InvoicePayment.CardType,
                        CardNumber = due.Receipt.InvoicePayment.CardNumber,
                        ExpDate = due.Receipt.InvoicePayment.ExpDate,
                        CVV = due.Receipt.InvoicePayment.CVV,
                        Last4Digits = due.Receipt.InvoicePayment.Last4Digits,
                        BillingFirstName = due.Receipt.InvoicePayment.BillingFirstName,
                        BillingLastName = due.Receipt.InvoicePayment.BillingLastName,
                        BillingAddr1 = due.Receipt.InvoicePayment.BillingAddr1,
                        BillingAddr2 = due.Receipt.InvoicePayment.BillingAddr2,
                        BillingCity = due.Receipt.InvoicePayment.BillingCity,
                        BillingState = due.Receipt.InvoicePayment.BillingState,
                        BillingPostalCode = due.Receipt.InvoicePayment.BillingPostalCode
                    };
                    context.Receipts.Add(receipt);
                    context.SaveChanges();
                }
            }
        }
    }
}
