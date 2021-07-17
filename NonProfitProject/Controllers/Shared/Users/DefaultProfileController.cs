﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NonProfitProject.Models;
using NonProfitProject.Models.ViewModels.Shared.Users;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Code.Security;

namespace NonProfitProject.Controllers.Shared.Users
{
    public class DefaultProfileController : Controller
    {

        protected NonProfitContext context { get; set; }
        protected UserManager<User> userManager;
        protected SignInManager<User> signInManager;

        //Edit login for user, if admin.
        [HttpGet]
        public async Task<IActionResult> EditLogin()
        {
            var user = await userManager.GetUserAsync(User);
            var EditLoginViewModel = new EditLoginViewModel { userID = user.Id, Email = user.Email, Username = user.UserName, ReceiveWeeklyNewsletter = user.ReceiveWeeklyNewsletter, CurrentPassword = "", NewPassword = "", NewPasswordConfirmed = "" };
            return View("EditLogin", EditLoginViewModel);
        }
        public async Task<IActionResult> EditLogin(EditLoginViewModel model)
        {
            if(User.FindFirstValue(ClaimTypes.NameIdentifier) != model.userID)
            {
                model.userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ModelState.AddModelError("", "ERROR - Invalid UserID");    
            }
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.userID);
                var isPasswordCorrect = await userManager.CheckPasswordAsync(user, model.CurrentPassword);
                if (isPasswordCorrect)
                {
                    user.Email = model.Email;
                    user.UserName = model.Username;
                    user.ReceiveWeeklyNewsletter = model.ReceiveWeeklyNewsletter;
                    if (model.NewPassword != null)
                    {
                        var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                        if (!result.Succeeded)
                        {
                            foreach (IdentityError i in result.Errors)
                            {
                                ModelState.AddModelError("", i.Description);
                            }
                            return View();
                        }
                    }
                    context.Users.Update(user);
                    context.SaveChanges();
                    TempData["ChangesSaved"] = "Your login information has been Saved";
                }
                else
                {
                    ModelState.AddModelError("", "Password is incorrect");
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var ProfileViewModel = new ProfileViewModel { UserID = user.Id, FirstName=user.UserFirstName, LastName=user.UserLastName, Email = user.Email, Username = user.UserName, Addr1 = user.UserAddr1, Addr2 = user.UserAddr2, City = user.UserCity, PostalCode = user.UserPostalCode, State = user.UserState, BirthDate = user.UserBirthDate, Gender = user.UserGender, CreationDate=user.UserCreationDate, LastActivity=user.UserLastActivity };
            return View(ProfileViewModel);
        }
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserID);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddPayment()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddPayment(SavedPaymentViewModel model)
        {
            if (model.usingAccountAddress)
            {
                User user = context.Users.Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking().FirstOrDefault();
                model.BillingFirstName = user.UserFirstName;
                ModelState.Remove("BillingFirstName");
                model.BillingLastName = user.UserLastName;
                ModelState.Remove("BillingLastName");
                model.BillingAddr1 = user.UserAddr1;
                ModelState.Remove("BillingAddr1");
                model.BillingAddr2 = user.UserAddr2;
                ModelState.Remove("BillingAddr2");
                model.BillingCity = user.UserCity;
                ModelState.Remove("BillingCity");
                model.BillingState = user.UserState;
                ModelState.Remove("BillingState");
                model.BillingPostalCode = user.UserPostalCode;
                ModelState.Remove("BillingPostalCode");

            }
            AesEncryption aes = new AesEncryption();
            //check if the user already has this card in their saved payments
            var test = context.SavedPayments.Where(sp => sp.CardNumber.Equals(aes.Encrypt(model.CardNumber)) &&
                sp.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking().FirstOrDefault();
            if (test != null)
            {
                ModelState.AddModelError("", "Error - This card has already been added");
            }
            if (ModelState.IsValid)
            {
                //used to encrypt sensitive ionformation
                
                SavedPaymentInformation savedPaymentInformation = new SavedPaymentInformation()
                {
                    UserID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CardholderName = model.CardholderName,
                    CardType = model.CardType,
                    CardNumber = aes.Encrypt(model.CardNumber),
                    ExpDate = model.CardExpDate,
                    CVV = aes.Encrypt(model.CardCVV),
                    BillingFirstName = model.BillingFirstName,
                    BillingLastName = model.BillingLastName,
                    BillingAddr1 = model.BillingAddr1,
                    BillingAddr2 = model.BillingAddr2,
                    BillingCity = model.BillingCity,
                    BillingState = model.BillingState,
                    BillingPostalCode = model.BillingPostalCode,
                    Last4Digits = Int32.Parse(model.CardNumber.Substring(model.CardNumber.Length - 4))
                    
                };
                context.SavedPayments.Add(savedPaymentInformation);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
