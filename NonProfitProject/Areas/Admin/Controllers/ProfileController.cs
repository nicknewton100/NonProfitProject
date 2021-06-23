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

        public IActionResult Index()
        {
            /*var users = context.Users.ToList();
            foreach(User u in users)
            {
                context.Users.Attach(u).Property(x => x.recieveWeeklyNewsletter).IsModified = true;
                u.recieveWeeklyNewsletter = false;
                context.SaveChanges();
            }*/
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> EditLogin()
        {
            var user = await userManager.GetUserAsync(User);
            var EditLoginViewModel = new EditLoginViewModel { userID = user.Id,Email = user.Email, EmailConfirmed = "", Username = user.UserName, recieveWeeklyNewsletter = user.recieveWeeklyNewsletter, CurrentPassword = "", NewPassword = "", NewPasswordConfirmed = "" };
            return View("EditLogin",EditLoginViewModel);
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
                    user.recieveWeeklyNewsletter = model.recieveWeeklyNewsletter;
                    if (model.NewPassword != null)
                    {
                        var result = await userManager.ChangePasswordAsync(user,model.CurrentPassword, model.NewPassword);
                        if (!result.Succeeded)
                        {
                            foreach(IdentityError i in result.Errors)
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
    }
}
