using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NonProfitProject.Models;
using NonProfitProject.Areas.Admin.Models.ViewModels;

namespace NonProfitProject.Areas.Admin.Controllers
{
    //If admin, show this page
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProfileController : Controller
    {

        private NonProfitContext context { get; set; }

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager, NonProfitContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> EditLogin()
        {
            var user = await userManager.GetUserAsync(User);
            var EditLoginViewModel = new EditLoginViewModel { userID = user.Id, Email = user.Email, EmailConfirmed = "", Username = user.UserName, receiveWeeklyNewsletter = user.ReceiveWeeklyNewsletter, CurrentPassword = "", NewPassword = "", NewPasswordConfirmed = "" };
            return View("EditLogin", EditLoginViewModel);
        }
        public async Task<IActionResult> EditLogin(EditLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.userID);
                var isPasswordCorrect = await userManager.CheckPasswordAsync(user, model.CurrentPassword);
                if (isPasswordCorrect)
                {
                    user.Email = model.Email;
                    user.UserName = model.Username;
                    user.ReceiveWeeklyNewsletter = model.receiveWeeklyNewsletter;
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
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var ProfileViewModel = new ProfileViewModel { UserID = user.Id, FirstName=user.UserFirstName, LastName=user.UserLastName, Email = user.Email, Username = user.UserName, Addr1 = user.UserAddr1, Addr2 = user.UserAddr2, City = user.UserCity, PostalCode = user.UserPostalCode, Country = user.UserCountry, State = user.UserState, BirthDate = user.UserBirthDate, Gender = user.UserGender, CreationDate=user.UserCreationDate, LastActivity=user.UserLastActivity };
            return View("Index", ProfileViewModel);
        }
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserID);
            }
            return View();
        }
    }
}
