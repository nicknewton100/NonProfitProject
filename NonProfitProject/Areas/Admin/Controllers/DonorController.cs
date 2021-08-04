using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    //If user is admin, shows this page.
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]s/[action]/{id?}")]
    public class DonorController : Controller
    {
        private readonly NonProfitContext context;
        private readonly UserManager<User> userManager;
        public DonorController(NonProfitContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        //Shows Users Table List that are not One-TimeDonation as this is the account that stores Non-authenticated user donations 
        [Route("~/[area]/[controller]s")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUsersInRoleAsync("User");
            var users = context.Users.Where(u => u.UserName != "One-TimeDonation" && user.Contains(u) && !context.Employees.Any(e => e.UserID == u.Id)).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).ToList();
            return View(users);
        }
        //shows the add donor view page
        [HttpGet]
        public IActionResult AddDonor()
        {
            return View();
        }
        //adds the donor based on the information if its valid
        [HttpPost]
        public async Task<IActionResult> AddDonor(DonorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    UserFirstName = model.Firstname,
                    UserLastName = model.Lastname,
                    UserGender = model.Gender,
                    UserBirthDate = (DateTime)model.BirthDate,
                    UserAddr1 = model.Addr1,
                    UserAddr2 = model.Addr2,
                    UserCity = model.City,
                    UserState = model.State,
                    UserPostalCode = (int)model.PostalCode,
                    ReceiveWeeklyNewsletter = false,
                    UserCreationDate = DateTime.UtcNow,
                    UserLastActivity = DateTime.UtcNow,
                    AccountDisabled = false,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await userManager.CreateAsync(user, model.TemporaryPassword);
                //after successfully creating account, redirect user to homepage.
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    TempData["DonorChanges"] = String.Format("{0} has been created", user.UserFirstName + "" + user.UserLastName);
                    return RedirectToAction("Index");                                   
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
        //deltes the donor based on routed id if it exists
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = context.Users.Find(id);
            if(user == null)
            {
                TempData["DonorChanges"] = String.Format("Donor with UserID {0} does not exist", id);
                return RedirectToAction("Index");
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["DonorChanges"] = String.Format("{0} has been deleted", user.UserFirstName + "" + user.UserLastName);
            }
            return RedirectToAction("Index");
        }
        //shows deatils for the Donor if the donor exists
        [HttpGet]
        public IActionResult Details(string id)
        {
            ViewBag.Action = "Details";
            var user = context.Users.Find(id);
            if(user == null)
            {
                RedirectToAction("Index");
            }
            DonorViewModel model = new DonorViewModel()
            {
                id = user.Id,
                Firstname = user.UserFirstName,
                Lastname = user.UserLastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.UserGender,
                BirthDate = user.UserBirthDate,
                Addr1 = user.UserAddr1,
                Addr2 = user.UserAddr2,
                City = user.UserCity,
                State = user.UserState,
                PostalCode = user.UserPostalCode
            };
            return View("EditDonor", model);
        }
        //shows edit donor page based on id if the user exists
        [HttpGet]
        public IActionResult EditDonor(string id)
        {
            ViewBag.Action = "Edit";
            var user = context.Users.Find(id);
            if (user == null)
            {
                RedirectToAction("Index");
            }
            DonorViewModel model = new DonorViewModel()
            {
                id = user.Id,
                Firstname = user.UserFirstName,
                Lastname = user.UserLastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.UserGender,
                BirthDate = user.UserBirthDate,
                Addr1 = user.UserAddr1,
                Addr2 = user.UserAddr2,
                City = user.UserCity,
                State = user.UserState,
                PostalCode = user.UserPostalCode
            };
            return View(model);
        }
        //makes chnages to the donor if the user exists. Also doesnt change login information unless the checkbox is checked
        [HttpPost]
        public async Task<IActionResult> EditDonor(DonorViewModel model)
        {
            var user = context.Users.Find(model.id);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid UserID");
            }
            if (model.TemporaryPassword == null || !model.IsChangingLogininformation)
            {
                ModelState.Remove("TemporaryPassword");
                ModelState.Remove("TemporaryPasswordConfirmed");
            }
            if (ModelState.IsValid)
            {
                if (model.IsChangingLogininformation)
                {
                    user.UserName = model.Username;
                    user.Email = model.Email;
                }
                user.UserFirstName = model.Firstname;
                user.UserLastName = model.Lastname;
                user.UserGender = model.Gender;
                user.UserBirthDate = (DateTime)model.BirthDate;
                user.UserAddr1 = model.Addr1;
                user.UserAddr2 = model.Addr2;
                user.UserCity = model.City;
                user.UserState = model.State;
                user.UserPostalCode = (int)model.PostalCode;
                user.PhoneNumber = model.PhoneNumber;
                user.UserLastActivity = DateTime.UtcNow;

                if (model.TemporaryPassword != null && model.IsChangingLogininformation)
                {
                    var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordChange = await userManager.ResetPasswordAsync(user, resetToken, model.TemporaryPassword);
                    if (!passwordChange.Succeeded)
                    {
                        foreach (IdentityError i in passwordChange.Errors)
                        {
                            ModelState.AddModelError("TemporaryPassword", i.Description);
                        }
                        return View();
                    }
                }
                var updateUser = await userManager.UpdateAsync(user);
                if (updateUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    TempData["DonorChanges"] = String.Format("{0} has been updated", user.UserFirstName + " " + user.UserLastName);
                    return RedirectToAction("Details", new {id = user.Id });
                } 
            }
            ViewBag.Action = "Edit";
            return View(model);
        }
    }
}
