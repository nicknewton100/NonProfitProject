using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Code;
using NonProfitProject.Code.Security;
using NonProfitProject.Models;
using NonProfitProject.Models.Extensions;
using NonProfitProject.Models.ViewModels;
using NonProfitProject.Models.ViewModels.Shared.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Controllers.Shared.Users
{
    //used in every area so it is just inherited to all of them
    public class DefaultMembershipController : Controller
    {
        protected NonProfitContext context;
        protected UserManager<User> userManager;
        protected IEmailManager emailManager;

        //if the user hasnt signed up as a member, it send them to the sign up page. Ese, it shows all the membership information as well as all their membership dues and how long they have been a member for consecutively
        public async Task<IActionResult> Index() 
        {
            var currentUser = await userManager.GetUserAsync(User);
            if(!await userManager.IsInRoleAsync(currentUser, "Member"))
            {
                return RedirectToAction("SignUp");
            }
            var receipts = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null && r.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).OrderByDescending(r => r.Date).ToList();
            var userMembership = context.MembershipDues.Include(md => md.MembershipType).Where(md => md.UserID == currentUser.Id).OrderBy(md => md.MemDuesID).ToList();          
            if(receipts.Count() != 0)
            {
                var memberSince = MembershipDues.GetConsecutiveDate(userMembership);
                TimeSpan timespan = (TimeSpan)(DateTime.UtcNow - memberSince);
                ViewBag.TimeSpan = new List<int>
                {
                    //years
                    (int)Math.Floor(timespan.TotalDays / 365),
                    //motnhs
                    (int)Math.Floor((timespan.TotalDays / 30) % 12),
                    //days
                    (int)(timespan.Days) % 30,
                    //timespan.Days,
                    timespan.Hours,
                    timespan.Minutes,
                    timespan.Seconds
                };
                return View(receipts);
            }           
            return View("LifetimeMembership");
            
        }
        //displays detais about membership due
        public IActionResult Details(int id)
        {
            var membership = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null && r.ReceiptID == id).OrderBy(r => r.Date).FirstOrDefault();
            if (membership == null)
            {
                return RedirectToAction("Index");
            }
            EditMembershipDueViewModel model = new EditMembershipDueViewModel()
            {
                ReceiptID = membership.ReceiptID,
                Username = membership.User.UserName,
                Total = membership.MembershipDue.MembershipType.Amount,
                FirstName = membership.User.UserFirstName,
                LastName = membership.User.UserLastName,
                Email = membership.User.Email,
                Phone = membership.User.PhoneNumber,
                MembershipType = membership.MembershipDue.MembershipType.Name,
                StartDate = membership.MembershipDue.MemStartDate,
                EndDate = membership.MembershipDue.MemEndDate,
                RenewalDate = membership.MembershipDue.MemRenewalDate,
                CancelDate = membership.MembershipDue.MemCancelDate,
                Active = membership.MembershipDue.MemActive,
                CardholderName = membership.InvoicePayment.CardholderName,
                CardNumber = membership.InvoicePayment.Last4Digits.ToString(),
                CardType = membership.InvoicePayment.CardType,
                ExpDate = membership.InvoicePayment.ExpDate,
                BillingFirstName = membership.InvoicePayment.BillingFirstName,
                BillingLastName = membership.InvoicePayment.BillingLastName,
                BillingAddr1 = membership.InvoicePayment.BillingAddr1,
                BillingAddr2 = membership.InvoicePayment.BillingAddr2,
                BillingCity = membership.InvoicePayment.BillingCity,
                BillingState = membership.InvoicePayment.BillingState,
                BillingPostalCode = membership.InvoicePayment.BillingPostalCode

            };
            return View(model);
        }

        //displays signup page
        [HttpGet]
        public IActionResult SignUp()
        {           
            return View();
        }
        //if the user isnt already a member, it gets the membership type baed on name and sets it to a news membership due which is held in the session. if the user come back to this page, it will edit their information but not cancel out what they did.
        [HttpPost]
        public async Task<IActionResult> Signup(string name)
        {
            if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index");
            }
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

            if (sessionModel == null || sessionModel.PaymentViewModel == null)
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
        //if the user is not already a member and if the user doesnt have a session object, it will displays the payment page. If either one of those are false, it will send back to the main page.
        [HttpGet]
        public IActionResult Payment()
        {
            if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index");
            }
            if (HttpContext.Session.GetObject<SignupMembershipViewModel>("SignupMembershipModel") == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //sets the payment in the session if the user is not a member and if the session object exists. Also can come back to this page without it editing other information
        [HttpPost]
        public IActionResult Payment(DonationPaymentViewModel model)
        {
            if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index");
            }
            var sessionModel = HttpContext.Session.GetObject<SignupMembershipViewModel>("SignupMembershipModel");
            if (sessionModel == null)
            {
                TempData["SessionTimeout"] = "Session has timed out";
                return RedirectToAction("Index");
            }
            if (DateTime.ParseExact(model.ExpDate, "MM/yy", CultureInfo.InvariantCulture) < DateTime.UtcNow)
            {
                ModelState.AddModelError("ExpDate", "This payment method has expired");
            }
            if (model.CardNumber.Length != 19)
            {
                ModelState.AddModelError("CardNumber", "This payment is incomplete");
            }
            if (model.CVV.Length != 3)
            {
                ModelState.AddModelError("CVV", "CVV requires 3 digits");
            }
            if (ModelState.IsValid)
            {
                sessionModel.PaymentViewModel = model;
                HttpContext.Session.SetObject("SignupMembershipModel", sessionModel);
                return RedirectToAction("CheckOut");
            }
            return View();
        }

        //displays all checkout information beore checking out
        public IActionResult CheckOut()
        {
            if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index");
            }
            var sessionModel = HttpContext.Session.GetObject<SignupMembershipViewModel>("SignupMembershipModel");
            if (sessionModel == null)
            {
                TempData["SessionTimeout"] = "Session has timed out";
                return RedirectToAction("SignUp");
            }
            return View();
        }
        //places the order and adds all the data held in the session to the database and sets the user as a member
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index");
            }
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
            await userManager.RemoveFromRoleAsync(user, "User");
            var message = emailManager.CreateSimpleMessage(String.Format("{0} Membership for Non-Paw-Fit Animal Rescue",sessionModel.Membership.MembershipType), String.Format("Thank you, {0}, for becoming a {1} member of the Non-Paw-Fit Animal Rescue Foundation! You're continual efforts to create a better life for all animals will not go unnoticed. \n\n    Receipt Information: \n        Receipt ID: {2} \n        Total: {3:C} \n        Date: {4} \n\n    Membership Information: \n        Membership ID: {5} \n        Membership purchase: {1} \n        Payment per Month: {6:C} \n        Membership for User: {7} \n        Start Date: {8} \n        End Date: {9} \n        Renewal Date: {10} \n\n    Card Information: \n        CardHolder Name: {11} \n        Card Type: {12} \n        Card Number: xxxx-xxxx-xxxx-{13} \n        Expiration Date: {14} \n    Billing Information \n        Name: {15} \n        Address 1: {16} \n        Address 2: {17} \n        City: {18} \n        State: {19} \n        Postal Code: {20}", receipt.User.UserFirstName + " " + receipt.User.UserLastName, sessionModel.Membership.MembershipType.Name , receipt.ReceiptID, receipt.Total, receipt.Date, receipt.MembershipDue.MembershipTypeID, sessionModel.Membership.MembershipType.Amount, receipt.User.UserName, receipt.MembershipDue.MemStartDate, receipt.MembershipDue.MemEndDate, receipt.MembershipDue.MemRenewalDate, receipt.InvoicePayment.CardholderName, receipt.InvoicePayment.CardType, receipt.InvoicePayment.Last4Digits, receipt.InvoicePayment.ExpDate, receipt.InvoicePayment.BillingFirstName + " " + receipt.InvoicePayment.BillingLastName, receipt.InvoicePayment.BillingAddr1, receipt.InvoicePayment.BillingAddr2, receipt.InvoicePayment.BillingCity, receipt.InvoicePayment.BillingState, receipt.InvoicePayment.BillingPostalCode));

            emailManager.SendEmail(new User() { UserFirstName = receipt.User.UserFirstName, Email = receipt.User.Email }, message);
            return RedirectToAction("Index");
        }
        //cancel order removes the session object from the session and starts you at the beginning
        [HttpPost]
        public IActionResult CancelOrder()
        {
            HttpContext.Session.Remove("SignupMembershipModel");
            return RedirectToAction("Index");
        }
        //cancel membership cancels the membership which sets the last membership due to inactive and sets canceled date. as well as removes the user from member
        public async Task<IActionResult> CancelMembership()
        {
            var membership = context.MembershipDues.Where(md => md.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier) && md.MemActive == true).OrderBy(md => md.MemDuesID).LastOrDefault();
            if(membership == null)
            {
                TempData["MembershipManagment"] = String.Format("A membership asssociated with ID {0} is invalid", membership.MemDuesID);
                return RedirectToAction("Index");
            }
            membership.MemCancelDate = DateTime.UtcNow;
            membership.MemActive = false;
            var user = await userManager.GetUserAsync(User);
            await userManager.RemoveFromRoleAsync(user, "Member");
            if (!await userManager.IsInRoleAsync(user, "Admin") && !await userManager.IsInRoleAsync(user, "Employee"))
            {
                await userManager.AddToRoleAsync(user, "User");
            }
            return RedirectToAction("Signup");
        }
    }
}
