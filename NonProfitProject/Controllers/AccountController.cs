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

namespace NonProfitProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private NonProfitContext context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, NonProfitContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        //Login
        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View();
        }

        //Creates login verification feature for the user
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
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        var currentUser = await userManager.FindByNameAsync(model.Username);
                        if (await userManager.IsInRoleAsync(currentUser, "Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else if (await userManager.IsInRoleAsync(currentUser, "Employee"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Employee" });
                        }
                        else if (await userManager.IsInRoleAsync(currentUser, "Member"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Member" });
                        }
                        else if (await userManager.IsInRoleAsync(currentUser, "User"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Users" });
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid username/password.");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Creates a new user
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
                    AccountDisabled = false
                };

                var result = await userManager.CreateAsync(user, model.Password);
                //after successfully creating account, redirect user to homepage.
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    EmailManager emailmanager = new EmailManager(context);
                    var emailmessage = emailmanager.CreateSimpleMessage("BankdTechSolutions Sign-up Confirmation",String.Format("Hey {0}, \n \n Thank you for signing up at BankdTechSolutions.net! We are excited to have a new member that can contribute to our Non-Profit cause. You're one step closer to becoming a saint! Have a great day!",user.UserFirstName + " " + user.UserLastName));
                    emailmanager.SendEmail(user,emailmessage);

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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
