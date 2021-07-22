using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private NonProfitContext context;
        private UserManager<User> userManager;
        public DonorController(NonProfitContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        //Shows Users Table List
        [Route("~/[area]/[controller]s")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUsersInRoleAsync("User");
            var members = await userManager.GetUsersInRoleAsync("Member");
            //queries users that are not in the employee table
            //var users = context.Users.Where(u => !context.Employees.Any(e => u.Id == e.UserID || u.UserName == "One-TimeDonation")).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).ToList();

            var users = context.Users.Where(u => u.UserName != "One-TimeDonation" && user.Contains(u) || members.Contains(u)).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).ToList();
            return View(users);
        }
        public IActionResult AddDonor()
        {
            return View();
        }

        //////////////////////////////////////////////////////////////////////
        //Edit 2 pieces of code below to appropriate where to return view from
        [HttpGet]
        public IActionResult EditDonor(int userID)
        {
            ViewBag.Action = "Edit";
            var User = context.Users.Find(userID);
            return View(User);
        }

        [HttpPost]
        public IActionResult EditDonor(User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }
    }
}
