using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NonProfitProject.Code;
using NonProfitProject.Code.Security;
using NonProfitProject.Models;
using NonProfitProject.Models.Extensions;
using NonProfitProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Controllers
{
    public class DonateController : Controller
    {
        private NonProfitContext context;
        public DonateController(NonProfitContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Donate(DonationViewModel model)
        {
            if(model.DonationAmount == 0.00m)
            {
                ModelState.AddModelError("DonationAmount", "Please select an amount to donate");
            }
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
                model.FirstName = user.UserFirstName;
                ModelState.Remove("FirstName");
                model.LastName = user.UserLastName;
                ModelState.Remove("LastName");
                model.Email = user.Email;
                ModelState.Remove("Email");
                model.Phone = user.PhoneNumber;
                ModelState.Remove("Phone");
                model.Addr1 = user.UserAddr1;
                ModelState.Remove("Addr1");
                model.Addr2 = user.UserAddr2;
                ModelState.Remove("Addr2");
                model.City = user.UserCity;
                ModelState.Remove("City");
                model.State = user.UserState;
                ModelState.Remove("State");
                model.PostalCode = user.UserPostalCode;
                ModelState.Remove("PostalCode");
            }
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetObject("DonationInformation", model);
                return RedirectToAction("Payment");
            }
            return View("Index");
        }
        [HttpGet]
        public IActionResult Payment()
        {
            if(HttpContext.Session.GetObject<DonationViewModel>("DonationInformation") == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Payment(DonationPaymentViewModel model)
        {
            var DonationInformation = HttpContext.Session?.GetObject<DonationViewModel>("DonationInformation") ?? null;
            if (DonationInformation == null)
            {
                TempData["SessionTimeout"] = "Session has timed out - Please re-enter Donation Information";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Receipts receipt = new Receipts()
                {
                    UserID = (User.Identity.IsAuthenticated) ? User.FindFirstValue(ClaimTypes.NameIdentifier) : context.Users.Where(u => u.UserName == "One-TimeDonation").FirstOrDefault().Id,
                    Total = (decimal)DonationInformation.DonationAmount,
                    Date = DateTime.UtcNow,
                    Description = "Donation"
                };
                receipt.Donation = new Donations()
                {
                    UserID = (User.Identity.IsAuthenticated) ? User.FindFirstValue(ClaimTypes.NameIdentifier) : context.Users.Where(u => u.UserName == "One-TimeDonation").FirstOrDefault().Id,
                    DonationAmount = (decimal)DonationInformation.DonationAmount,
                    DonationDate = DateTime.UtcNow
                };
                receipt.InvoiceDonorInformation = new InvoiceDonorInformation()
                {
                    FirstName = DonationInformation.FirstName,
                    LastName = DonationInformation.LastName,
                    Email = DonationInformation.Email,
                    Phone = DonationInformation.Phone,
                    Addr1 = DonationInformation.Addr1,
                    Addr2 = DonationInformation.Addr2,
                    City = DonationInformation.City,
                    State = DonationInformation.State,
                    PostalCode = DonationInformation.PostalCode
                };
                AesEncryption aes = new AesEncryption();
                receipt.InvoicePayment = new InvoicePayment()
                {
                    CardholderName = model.CardholderName,
                    CardType = model.CardType,
                    CardNumber = aes.Encrypt(model.CardNumber),
                    ExpDate = model.ExpDate,
                    CVV = aes.Encrypt(model.CVV),
                    Last4Digits = Int32.Parse(model.CardNumber.Substring(model.CardNumber.Length - 4)),
                    BillingFirstName = model.BillingFirstName,
                    BillingLastName = model.BillingLastName,
                    BillingAddr1 = model.BillingAddr1,
                    BillingAddr2 = model.BillingAddr2,
                    BillingCity = model.BillingCity,
                    BillingState = model.BillingState,
                    BillingPostalCode = model.BillingPostalCode
                };
                context.Receipts.Add(receipt);
                //HttpContext.Session.SetObject("ReviewOrder", receipt);
                //checks to see if the user wants to save payments. If they do, it checks to see if the card has already been saved. if it has, it doesn't save the payment
                if(model.savePayment == true && context.SavedPayments.Where(sp => sp.CardNumber.Equals(aes.Encrypt(model.CardNumber)) && sp.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking().FirstOrDefault() == null)
                {
                    context.SavedPayments.Add(new SavedPaymentInformation()
                    {
                        UserID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        CardholderName = model.CardholderName,
                        CardType = model.CardType,
                        CardNumber = aes.Encrypt(model.CardNumber),
                        ExpDate = model.ExpDate,
                        CVV = aes.Encrypt(model.CVV),
                        Last4Digits = Int32.Parse(model.CardNumber.Substring(model.CardNumber.Length - 4)),
                        BillingFirstName = model.BillingFirstName,
                        BillingLastName = model.BillingLastName,
                        BillingAddr1 = model.BillingAddr1,
                        BillingAddr2 = model.BillingAddr2,
                        BillingCity = model.BillingCity,
                        BillingState = model.BillingState,
                        BillingPostalCode = model.BillingPostalCode
                    });       
                }
                context.SaveChanges();
                EmailManager email = new EmailManager(context);
                var message = email.CreateSimpleMessage("Donation to Non-Paw-Fit Animal Rescue", String.Format("Thank you, {0}, for donating to Non-Paw-Fit Animal rescue! You're Donation of {1:C} will not go unnoticed. \n\n    Receipt Information: \n        Receipt ID: {2} \n        Total: {1:C} \n        Date: {3} \n\n    Donor Information: \n        Name: {0} \n        Address 1: {4} \n        Address 2: {5} \n        City: {6} \n        State: {7} \n        Postal Code: {8} \n\n    Card Information: \n        CardHolder Name: {9} \n        Card Type: {10} \n        Card Number: xxxx-xxxx-xxxx-{11} \n        Expiration Date: {12} \n    Billing Information \n        Name: {13} \n        Address 1: {14} \n        Address 2: {15} \n        City: {16} \n        State: {17} \n        Postal Code: {18}", receipt.InvoiceDonorInformation.FirstName + " " + receipt.InvoiceDonorInformation.LastName, receipt.Total, receipt.ReceiptID, receipt.Date, receipt.InvoiceDonorInformation.Addr1, receipt.InvoiceDonorInformation.Addr2, receipt.InvoiceDonorInformation.City, receipt.InvoiceDonorInformation.State, receipt.InvoiceDonorInformation.PostalCode, receipt.InvoicePayment.CardholderName, receipt.InvoicePayment.CardType, receipt.InvoicePayment.Last4Digits, receipt.InvoicePayment.ExpDate, receipt.InvoicePayment.BillingFirstName + " " + receipt.InvoicePayment.BillingLastName, receipt.InvoicePayment.BillingAddr1, receipt.InvoicePayment.BillingAddr2, receipt.InvoicePayment.BillingCity, receipt.InvoicePayment.BillingState, receipt.InvoicePayment.BillingPostalCode));

                email.SendEmail(new User() { UserFirstName = receipt.InvoiceDonorInformation.FirstName, Email = receipt.InvoiceDonorInformation.Email }, message);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
