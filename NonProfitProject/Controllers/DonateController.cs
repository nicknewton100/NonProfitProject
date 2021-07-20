using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
                DonationViewModel model = new DonationViewModel()
                {
                    FirstName = user.UserFirstName,
                    LastName = user.UserLastName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Addr1 = user.UserAddr1,
                    Addr2 = user.UserAddr2,
                    City = user.UserCity,
                    State = user.UserState,
                    PostalCode = user.UserPostalCode
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Donate(DonationViewModel model)
        {
            if(model.DonationAmount == 0.00m)
            {
                ModelState.AddModelError("DonationAmount", "Please select an amount to donate");
            }
            if (ModelState.IsValid)
            {
                /*Receipts receipt = new Receipts()
                {
                    UserID = (User.Identity.IsAuthenticated) ? User.FindFirstValue(ClaimTypes.NameIdentifier) : context.Users.Where(u => u.UserName == "Anonymous").FirstOrDefault().Id,
                    Total = model.DonationAmount,
                    Date = DateTime.UtcNow,
                    Description = "Donation"
                };
                Donations donation = new Donations()
                {
                    UserID = (User.Identity.IsAuthenticated) ? User.FindFirstValue(ClaimTypes.NameIdentifier) : context.Users.Where(u => u.UserName == "Anonymous").FirstOrDefault().Id,
                    DonationAmount = model.DonationAmount,
                    DonationDate = DateTime.UtcNow
                };
                InvoiceDonorInformation invoiceDonorInformation = new InvoiceDonorInformation()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Addr1 = model.Addr1,
                    Addr2 = model.Addr2,
                    City = model.City,
                    State = model.State,
                    PostalCode = model.PostalCode
                };*/
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
                    UserID = (User.Identity.IsAuthenticated) ? User.FindFirstValue(ClaimTypes.NameIdentifier) : context.Users.Where(u => u.UserName == "Anonymous").FirstOrDefault().Id,
                    Total = DonationInformation.DonationAmount,
                    Date = DateTime.UtcNow,
                    Description = "Donation"
                };
                Donations donation = new Donations()
                {
                    UserID = (User.Identity.IsAuthenticated) ? User.FindFirstValue(ClaimTypes.NameIdentifier) : context.Users.Where(u => u.UserName == "Anonymous").FirstOrDefault().Id,
                    DonationAmount = DonationInformation.DonationAmount,
                    DonationDate = DateTime.UtcNow
                };
                InvoiceDonorInformation invoiceDonorInformation = new InvoiceDonorInformation()
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
                InvoicePayment invoicePayment = new InvoicePayment()
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
                receipt.Donation = donation;
                receipt.InvoiceDonorInformation = invoiceDonorInformation;
                receipt.InvoicePayment = invoicePayment;
                context.Add(receipt);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
