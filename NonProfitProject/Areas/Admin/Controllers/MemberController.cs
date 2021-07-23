using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]s/[action]/{id?}")]
    public class MemberController : Controller
    {

        private readonly NonProfitContext context;
        private readonly UserManager<User> userManager;
        public MemberController(NonProfitContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var member = await userManager.GetUsersInRoleAsync("Member");
            var members = context.Users.Where(u => u.UserName != "One-TimeDonation" && member.Contains(u)).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).Include(u => u.MembershipDues).ToList();
            return View(members);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMember(string id)
        {
            var user = context.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                TempData["MemberChanges"] = String.Format("The UserID \"{0}\" does not exist", id);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MemberChanges"] = String.Format("{0} is no longer a member", user.UserFirstName + " " + user.UserLastName);
                var result = await userManager.RemoveFromRoleAsync(user, "Member");
                if (!result.Succeeded)
                {
                    String.Format("The User with ID \"{0}\" is not a member", id);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult EditMember()
        {
            return View();
        }
    }
}
