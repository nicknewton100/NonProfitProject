using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Code;
using NonProfitProject.Code.Security;
using NonProfitProject.Models;
using NonProfitProject.Models.Extensions;
using NonProfitProject.Models.ViewModels;
using NonProfitProject.Models.ViewModels.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Controllers.Shared.Users
{
    public class DefaultMembershipController : Controller
    {
        protected NonProfitContext context;
        protected UserManager<User> userManager;

        public async Task<IActionResult> Index() 
        {
            var currentUser = await userManager.GetUserAsync(User);
            if(!await userManager.IsInRoleAsync(currentUser, "Member"))
            {
                return RedirectToAction("SignUp");
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(string name)
        {
            var memType = context.MembershipTypes.Where(mt => mt.Name == name).FirstOrDefault();
            if(memType == null)
            {
                return View();
            }
            var user = await userManager.GetUserAsync(User);
            var membershipViewModel = new SignupMembershipViewModel {
                Membership = new MembershipDues()
                {
                    MembershipType = memType,
                    User = user
                }
            }; 

            var sessionModel = HttpContext.Session.GetObject<SignupMembershipViewModel>("SignupMembershipModel");

            if (sessionModel == null)
            {
                HttpContext.Session.SetObject("SignupMembershipModel", membershipViewModel);
                return RedirectToAction("Payment");
            }
            else
            {
                membershipViewModel.PaymentViewModel = sessionModel.PaymentViewModel;
                HttpContext.Session.SetObject("SignupMembershipModel", membershipViewModel);
                return RedirectToAction("CheckOut");
            }
                      
        }

        [HttpGet]
        public IActionResult Payment()
        {
            if (HttpContext.Session.GetObject<SignupMembershipViewModel>("SignupMembershipModel") == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpPost]
        public IActionResult Payment(DonationPaymentViewModel model)
        {
            var sessionModel = HttpContext.Session.GetObject<SignupMembershipViewModel>("SignupMembershipModel");
            if (sessionModel == null)
            {
                TempData["SessionTimeout"] = "Session has timed out";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                sessionModel.PaymentViewModel = model;
                HttpContext.Session.SetObject("SignupMembershipModel", sessionModel);
                return RedirectToAction("CheckOut");
            }
            return View();
        }

        public IActionResult CheckOut()
        {
            var sessionModel = HttpContext.Session.GetObject<SignupMembershipViewModel>("SignupMembershipModel");
            if (sessionModel == null)
            {
                TempData["SessionTimeout"] = "Session has timed out";
                return RedirectToAction("SignUp");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var sessionModel = HttpContext.Session.GetObject<SignupMembershipViewModel>("SignupMembershipModel");
            if (sessionModel == null)
            {
                TempData["SessionTimeout"] = "Session has timed out";
                return RedirectToAction("SignUp");
            }
            Receipts receipt = new Receipts()
            {
                UserID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Total = (decimal)sessionModel.Membership.MembershipType.Amount,
                Date = DateTime.UtcNow,
                Description = "Membership Due"
            };
            receipt.MembershipDue = new MembershipDues()
            {
                UserID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                MembershipTypeID = sessionModel.Membership.MembershipType.MembershipTypeID,
                MemStartDate = DateTime.UtcNow,
                MemEndDate = DateTime.UtcNow.AddMonths(1),
                MemRenewalDate = DateTime.UtcNow.AddMonths(1),
                MemActive = true,
            };
            AesEncryption aes = new AesEncryption();
            receipt.InvoicePayment = new InvoicePayment()
            {
                CardholderName = sessionModel.PaymentViewModel.CardholderName,
                CardType = sessionModel.PaymentViewModel.CardType,
                CardNumber = aes.Encrypt(sessionModel.PaymentViewModel.CardNumber),
                ExpDate = sessionModel.PaymentViewModel.ExpDate,
                CVV = aes.Encrypt(sessionModel.PaymentViewModel.CVV),
                Last4Digits = Int32.Parse(sessionModel.PaymentViewModel.CardNumber[^4..]),
                BillingFirstName = sessionModel.PaymentViewModel.BillingFirstName,
                BillingLastName = sessionModel.PaymentViewModel.BillingLastName,
                BillingAddr1 = sessionModel.PaymentViewModel.BillingAddr1,
                BillingAddr2 = sessionModel.PaymentViewModel.BillingAddr2,
                BillingCity = sessionModel.PaymentViewModel.BillingCity,
                BillingState = sessionModel.PaymentViewModel.BillingState,
                BillingPostalCode = sessionModel.PaymentViewModel.BillingPostalCode
            };
            context.Receipts.Add(receipt);
            //HttpContext.Session.SetObject("ReviewOrder", receipt);
            //checks to see if the user wants to save payments. If they do, it checks to see if the card has already been saved. if it has, it doesn't save the payment
            if (sessionModel.PaymentViewModel.savePayment == true && context.SavedPayments.Where(sp => sp.CardNumber.Equals(aes.Encrypt(sessionModel.PaymentViewModel.CardNumber)) && sp.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking().FirstOrDefault() == null)
            {
                context.SavedPayments.Add(new SavedPaymentInformation()
                {
                    UserID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CardholderName = sessionModel.PaymentViewModel.CardholderName,
                    CardType = sessionModel.PaymentViewModel.CardType,
                    CardNumber = aes.Encrypt(sessionModel.PaymentViewModel.CardNumber),
                    ExpDate = sessionModel.PaymentViewModel.ExpDate,
                    CVV = aes.Encrypt(sessionModel.PaymentViewModel.CVV),
                    Last4Digits = Int32.Parse(sessionModel.PaymentViewModel.CardNumber[^4..]),
                    BillingFirstName = sessionModel.PaymentViewModel.BillingFirstName,
                    BillingLastName = sessionModel.PaymentViewModel.BillingLastName,
                    BillingAddr1 = sessionModel.PaymentViewModel.BillingAddr1,
                    BillingAddr2 = sessionModel.PaymentViewModel.BillingAddr2,
                    BillingCity = sessionModel.PaymentViewModel.BillingCity,
                    BillingState = sessionModel.PaymentViewModel.BillingState,
                    BillingPostalCode = sessionModel.PaymentViewModel.BillingPostalCode
                });
            }
            context.SaveChanges();
            var user = await userManager.GetUserAsync(User);
            await userManager.AddToRoleAsync(user, "Member");
            EmailManager email = new EmailManager(context);
            var message = email.CreateSimpleMessage("Donation to Non-Paw-Fit Animal Rescue", String.Format("Thank you, {0}, for donating to Non-Paw-Fit Animal rescue! You're Donation of {1:C} will not go unnoticed. \n\n    Receipt Information: \n        Receipt ID: {2} \n        Total: {1:C} \n        Date: {3} \n\n    Membership Information: \n        Name: {0} \n        Address 1: {4} \n        Address 2: {5} \n        City: {6} \n        State: {7} \n        Postal Code: {8} \n\n    Card Information: \n        CardHolder Name: {9} \n        Card Type: {10} \n        Card Number: xxxx-xxxx-xxxx-{11} \n        Expiration Date: {12} \n    Billing Information \n        Name: {13} \n        Address 1: {14} \n        Address 2: {15} \n        City: {16} \n        State: {17} \n        Postal Code: {18}", receipt.InvoiceDonorInformation.FirstName + " " + receipt.InvoiceDonorInformation.LastName, receipt.Total, receipt.ReceiptID, receipt.Date, receipt.InvoiceDonorInformation.Addr1, receipt.InvoiceDonorInformation.Addr2, receipt.InvoiceDonorInformation.City, receipt.InvoiceDonorInformation.State, receipt.InvoiceDonorInformation.PostalCode, receipt.InvoicePayment.CardholderName, receipt.InvoicePayment.CardType, receipt.InvoicePayment.Last4Digits, receipt.InvoicePayment.ExpDate, receipt.InvoicePayment.BillingFirstName + " " + receipt.InvoicePayment.BillingLastName, receipt.InvoicePayment.BillingAddr1, receipt.InvoicePayment.BillingAddr2, receipt.InvoicePayment.BillingCity, receipt.InvoicePayment.BillingState, receipt.InvoicePayment.BillingPostalCode));

            //email.SendEmail(new User() { UserFirstName = receipt.InvoiceDonorInformation.FirstName, Email = receipt.InvoiceDonorInformation.Email }, message);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CancelOrder()
        {
            HttpContext.Session.Remove("SignupMembershipModel");
            return RedirectToAction("Index");
        }
    }
}
