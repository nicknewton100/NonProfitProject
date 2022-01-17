using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using NonProfitProject.Models;
using System.Security.Claims;
using System.Web;
using NonProfitProject.Code;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Employee.Data;

namespace NonProfitProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private NonProfitContext context;
        private IEmailManager emailManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, NonProfitContext context, IEmailManager emailManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.emailManager = emailManager;
        }

        //displays login page
        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View();
        }

        //Creates login verification feature for the user and logs the user in if the information is correct. Can login with email or username
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //If the user input an email, then it will check to see if a user with that email exists and if it does, it allows the user to signin through email
                var user = await userManager.FindByEmailAsync(model.Username);
                if(user != null)
                {
                    model.Username = user.UserName;
                }
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var currentUser = await userManager.FindByNameAsync(model.Username);
                    if(currentUser.AccountDisabled == true)
                    {
                        await signInManager.SignOutAsync();
                        return RedirectToAction("AccountDisabled");
                    }
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {  
                        if (await userManager.IsInRoleAsync(currentUser, "Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else if (await userManager.IsInRoleAsync(currentUser, "Employee"))
                        {
                            CommitteeStatus.GetName(context, User.FindFirstValue(ClaimTypes.NameIdentifier));
                            return RedirectToAction("Index", "Home", new { area = "Employee" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "Users" });
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid username/password.");
            }
            return View();
        }
        //displays register page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Creates a new user based on the model if the email and username are not already in use. Also sets role and sends an email when signing up
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(context.Users.Any(u => u.Email == model.Email)){
                    ModelState.AddModelError("", String.Format("The email address {0} is already in use", model.Email));
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    UserFirstName = model.Firstname,
                    UserLastName = model.Lastname,
                    UserGender = model.Gender,
                    UserBirthDate = model.BirthDate,
                    UserAddr1 = model.Addr1,
                    UserAddr2 = model.Addr2,
                    UserCity = model.City,
                    UserState = model.State,
                    UserPostalCode = model.PostalCode,
                    ReceiveWeeklyNewsletter = model.ReceiveWeeklyNewsletter,
                    UserCreationDate = DateTime.UtcNow,
                    UserLastActivity = DateTime.UtcNow,
                    AccountDisabled = false,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await userManager.CreateAsync(user, model.Password);
                //after successfully creating account, redirect user to homepage.
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    var emailmessage = emailManager.CreateSimpleMessage("BankdTechSolutions Sign-up Confirmation",String.Format("Hey {0}, \n \n Thank you for signing up at BankdTechSolutions.net! We are excited to have a new member that can contribute to our Non-Profit cause. You're one step closer to becoming a saint! Have a great day!",user.UserFirstName + " " + user.UserLastName));
                    emailManager.SendEmail(user,emailmessage);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }
        //logs out and clears tempdata and session data
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("Index", "Home");
        }
        //dsiplays account disabled if the user logs in and the account is disabled
        public IActionResult AccountDisabled()
        {
            return View();
        }
        //displays access denied if the user doesnt have access
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
