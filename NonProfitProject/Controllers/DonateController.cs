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
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Controllers
{
    public class DonateController : Controller
    {
        protected NonProfitContext context;
        public DonateController(NonProfitContext context)
        {
            this.context = context;
        }

        //displays donate page and fills in view is session object exists
        public IActionResult Index()
        {
            var DonationInformation = HttpContext.Session.GetObject<DonationViewModel>("DonationInformation");
            if (DonationInformation != null)
            {
                return View(DonationInformation);
            }
            return View();
        }

        //adds donation information to session. If user is authenticated, it uses the user iformation and removes the validation
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
                var DonationInformation = HttpContext.Session.GetObject<DonationViewModel>("DonationInformation");

                if(DonationInformation == null || DonationInformation.DonationPaymentViewModel == null)
                {
                    HttpContext.Session.SetObject("DonationInformation", model);
                    return RedirectToAction("Payment");
                }
                else
                {
                    DonationInformation.DonationAmount = model.DonationAmount;
                    DonationInformation.FirstName = model.FirstName;
                    DonationInformation.LastName = model.LastName;
                    DonationInformation.Email = model.Email;
                    DonationInformation.Phone = model.Phone;
                    DonationInformation.Addr1 = model.Addr1;
                    DonationInformation.Addr2 = model.Addr2;
                    DonationInformation.City = model.City;
                    DonationInformation.State = model.State;
                    DonationInformation.PostalCode = model.PostalCode;
                    HttpContext.Session.SetObject("DonationInformation", DonationInformation);
                    return RedirectToAction("CheckOut");
                }
                
            }
            return View("Index");
        }

        //shows the payment page 
        [HttpGet]
        public IActionResult Payment()
        {
            var DonationInformation = HttpContext.Session.GetObject<DonationViewModel>("DonationInformation");
            if (DonationInformation == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //sets the payment information in the session 
        [HttpPost]
        public IActionResult Payment(DonationPaymentViewModel model)
        {
            var DonationInformation = HttpContext.Session?.GetObject<DonationViewModel>("DonationInformation");
            if (DonationInformation == null)
            {
                TempData["SessionTimeout"] = "Session has timed out - Please re-enter Donation Information";
                return RedirectToAction("Index");
            }
            if (DateTime.ParseExact(model.ExpDate, "MM/yy", CultureInfo.InvariantCulture) < DateTime.ParseExact(DateTime.UtcNow.ToString("MM/yy"), "MM/yy", CultureInfo.InvariantCulture))
            {
                ModelState.AddModelError("ExpDate", "Payment method has expired");
            }
            if(model.CardNumber.Length != 19)
            {
                ModelState.AddModelError("CardNumber", "This payment is incomplete");
            }
            if(model.CVV.Length != 3)
            {
                ModelState.AddModelError("CVV", "CVV requires 3 digits");
            }
            if (ModelState.IsValid)
            {
                DonationInformation.DonationPaymentViewModel = model;
                HttpContext.Session.SetObject("DonationInformation", DonationInformation);
                return RedirectToAction("CheckOut");
            }
            return View();
        }
        //displays all information on the checkout page that is in the session
        [HttpGet]
        public IActionResult CheckOut()
        {
            if(HttpContext.Session.GetObject<DonationViewModel>("DonationInformation") == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //places the order which sends a confirmation email and adds the data to the database based on the session object
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var DonationInformation = HttpContext.Session.GetObject<DonationViewModel>("DonationInformation");
            if (HttpContext.Session.GetObject<DonationViewModel>("DonationInformation") == null)
            {
                return RedirectToAction("Index");
            }
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
                CardholderName = DonationInformation.DonationPaymentViewModel.CardholderName,
                CardType = DonationInformation.DonationPaymentViewModel.CardType,
                CardNumber = aes.Encrypt(DonationInformation.DonationPaymentViewModel.CardNumber),
                ExpDate = DonationInformation.DonationPaymentViewModel.ExpDate,
                CVV = aes.Encrypt(DonationInformation.DonationPaymentViewModel.CVV),
                Last4Digits = Int32.Parse(DonationInformation.DonationPaymentViewModel.CardNumber[^4..]),
                BillingFirstName = DonationInformation.DonationPaymentViewModel.BillingFirstName,
                BillingLastName = DonationInformation.DonationPaymentViewModel.BillingLastName,
                BillingAddr1 = DonationInformation.DonationPaymentViewModel.BillingAddr1,
                BillingAddr2 = DonationInformation.DonationPaymentViewModel.BillingAddr2,
                BillingCity = DonationInformation.DonationPaymentViewModel.BillingCity,
                BillingState = DonationInformation.DonationPaymentViewModel.BillingState,
                BillingPostalCode = DonationInformation.DonationPaymentViewModel.BillingPostalCode
            };
            context.Receipts.Add(receipt);
            //checks to see if the user wants to save payments. If they do, it checks to see if the card has already been saved. if it has, it doesn't save the payment
            if (DonationInformation.DonationPaymentViewModel.savePayment == true && context.SavedPayments.Where(sp => sp.CardNumber.Equals(aes.Encrypt(DonationInformation.DonationPaymentViewModel.CardNumber)) && sp.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking().FirstOrDefault() == null)
            {
                context.SavedPayments.Add(new SavedPaymentInformation()
                {
                    UserID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CardholderName = DonationInformation.DonationPaymentViewModel.CardholderName,
                    CardType = DonationInformation.DonationPaymentViewModel.CardType,
                    CardNumber = aes.Encrypt(DonationInformation.DonationPaymentViewModel.CardNumber),
                    ExpDate = DonationInformation.DonationPaymentViewModel.ExpDate,
                    CVV = aes.Encrypt(DonationInformation.DonationPaymentViewModel.CVV),
                    Last4Digits = Int32.Parse(DonationInformation.DonationPaymentViewModel.CardNumber[^4..]),
                    BillingFirstName = DonationInformation.DonationPaymentViewModel.BillingFirstName,
                    BillingLastName = DonationInformation.DonationPaymentViewModel.BillingLastName,
                    BillingAddr1 = DonationInformation.DonationPaymentViewModel.BillingAddr1,
                    BillingAddr2 = DonationInformation.DonationPaymentViewModel.BillingAddr2,
                    BillingCity = DonationInformation.DonationPaymentViewModel.BillingCity,
                    BillingState = DonationInformation.DonationPaymentViewModel.BillingState,
                    BillingPostalCode = DonationInformation.DonationPaymentViewModel.BillingPostalCode
                });
            }
            context.SaveChanges();
            EmailManager email = new EmailManager(context);
            var message = email.CreateSimpleMessage("Donation to Non-Paw-Fit Animal Rescue", String.Format("Thank you, {0}, for donating to Non-Paw-Fit Animal rescue! You're Donation of {1:C} will not go unnoticed. \n\n    Receipt Information: \n        Receipt ID: {2} \n        Total: {1:C} \n        Date: {3} \n\n    Donor Information: \n        Name: {0} \n        Address 1: {4} \n        Address 2: {5} \n        City: {6} \n        State: {7} \n        Postal Code: {8} \n\n    Card Information: \n        CardHolder Name: {9} \n        Card Type: {10} \n        Card Number: xxxx-xxxx-xxxx-{11} \n        Expiration Date: {12} \n    Billing Information \n        Name: {13} \n        Address 1: {14} \n        Address 2: {15} \n        City: {16} \n        State: {17} \n        Postal Code: {18}", receipt.InvoiceDonorInformation.FirstName + " " + receipt.InvoiceDonorInformation.LastName, receipt.Total, receipt.ReceiptID, receipt.Date, receipt.InvoiceDonorInformation.Addr1, receipt.InvoiceDonorInformation.Addr2, receipt.InvoiceDonorInformation.City, receipt.InvoiceDonorInformation.State, receipt.InvoiceDonorInformation.PostalCode, receipt.InvoicePayment.CardholderName, receipt.InvoicePayment.CardType, receipt.InvoicePayment.Last4Digits, receipt.InvoicePayment.ExpDate, receipt.InvoicePayment.BillingFirstName + " " + receipt.InvoicePayment.BillingLastName, receipt.InvoicePayment.BillingAddr1, receipt.InvoicePayment.BillingAddr2, receipt.InvoicePayment.BillingCity, receipt.InvoicePayment.BillingState, receipt.InvoicePayment.BillingPostalCode));

            email.SendEmail(new User() { UserFirstName = receipt.InvoiceDonorInformation.FirstName, Email = receipt.InvoiceDonorInformation.Email }, message);
            return RedirectToAction("ThankYou");
        }

        //cancels order and removes object from session
        [HttpPost]
        public IActionResult CancelOrder()
        {
            HttpContext.Session.Remove("DonationInformation");
            return RedirectToAction("Index", "Home");
        }
        //displays thak you page if session object isnt null
        public IActionResult ThankYou()
        {
            var DonationInformation = HttpContext.Session.GetObject<DonationViewModel>("DonationInformation");
            if (HttpContext.Session.GetObject<DonationViewModel>("DonationInformation") == null)
            {
                return RedirectToAction("Index");
            }
            HttpContext.Session.Remove("DonationInformation");
            return View(DonationInformation);
        }
    }
}
