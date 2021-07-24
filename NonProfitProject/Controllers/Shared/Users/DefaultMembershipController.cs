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
                MemStartDate = DateTime.UtcNow.Date,
                MemEndDate = DateTime.UtcNow.AddMonths(1).Date,
                MemRenewalDate = DateTime.UtcNow.AddMonths(1).Date,
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
            var message = email.CreateSimpleMessage(String.Format("{0} Membership for Non-Paw-Fit Animal Rescue",sessionModel.Membership.MembershipType), String.Format("Thank you, {0}, for becoming a {1} member of the Non-Paw-Fit Animal Rescue Foundation! You're continual efforts to create a better life for all animals will not go unnoticed. \n\n    Receipt Information: \n        Receipt ID: {2} \n        Total: {3:C} \n        Date: {4} \n\n    Membership Information: \n        Membership ID: {5} \n        Membership purchase: {1} \n        Payment per Month: {6:C} \n        Membership for User: {7} \n        Start Date: {8} \n        End Date: {9} \n        Renewal Date: {10} \n\n    Card Information: \n        CardHolder Name: {11} \n        Card Type: {12} \n        Card Number: xxxx-xxxx-xxxx-{13} \n        Expiration Date: {14} \n    Billing Information \n        Name: {15} \n        Address 1: {16} \n        Address 2: {17} \n        City: {18} \n        State: {19} \n        Postal Code: {20}", receipt.User.UserFirstName + " " + receipt.User.UserLastName, sessionModel.Membership.MembershipType.Name , receipt.ReceiptID, receipt.Total, receipt.Date, receipt.MembershipDue.MembershipTypeID, sessionModel.Membership.MembershipType.Amount, receipt.User.UserName, receipt.MembershipDue.MemStartDate, receipt.MembershipDue.MemEndDate, receipt.MembershipDue.MemRenewalDate, receipt.InvoicePayment.CardholderName, receipt.InvoicePayment.CardType, receipt.InvoicePayment.Last4Digits, receipt.InvoicePayment.ExpDate, receipt.InvoicePayment.BillingFirstName + " " + receipt.InvoicePayment.BillingLastName, receipt.InvoicePayment.BillingAddr1, receipt.InvoicePayment.BillingAddr2, receipt.InvoicePayment.BillingCity, receipt.InvoicePayment.BillingState, receipt.InvoicePayment.BillingPostalCode));

            email.SendEmail(new User() { UserFirstName = receipt.User.UserFirstName, Email = receipt.User.Email }, message);
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
