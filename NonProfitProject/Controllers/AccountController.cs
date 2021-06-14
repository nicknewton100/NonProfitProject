using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models.ViewModels;

namespace NonProfitProject.Controllers
{
    public class AccountController : Controller
    {
        //private UserManager<User> userManager;
        //private SignInManager<User> signInManager;

        //public AccountController(UserManager<User> userMangr, SignInManager<User> signInMangr)
        //{
        //    userManager = userMngr;
        //    signInManager = signInMngr;
        //}

        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> LogIn(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
        //    }
        //    if (result.Succeeded)
        //    {
        //        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //        {
        //            return Redirect(model.ReturnUrl);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    ModelState.AddModelError("", "Invalid username/password.");
        //    return AccountController(model);
        //}
        
    }
}
