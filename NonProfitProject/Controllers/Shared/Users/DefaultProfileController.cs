using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NonProfitProject.Models;
using NonProfitProject.Models.ViewModels.Shared.Users;
using System.Security.Claims;

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
        public IActionResult EditPayment()
        {
            return View();
        }
    }
}
