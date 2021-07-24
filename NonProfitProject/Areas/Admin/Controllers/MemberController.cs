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
                    TempData["MemberChanges"] = String.Format("The User with ID \"{0}\" is not a member", id);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddMembers()
        {
            var users = await userManager.GetUsersInRoleAsync("User");
            var usersModel = context.Users.Where(u => u.UserName != "One-TimeDonation" && users.Contains(u)).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddMembers(string id)
        {
            var user = context.Users.Find(id);
            if(user == null || !await userManager?.IsInRoleAsync(user, "User"))
            {
                TempData["MemberChanges"] = String.Format("The User with ID \"{0}\" is not a User", id);
                return RedirectToAction("AddMembers");
            }
            var result = await userManager.RemoveFromRoleAsync(user,"User");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Member");
                TempData["MemberChanges"] = String.Format("{0} is now a Member", user.UserFirstName + " " + user.UserLastName);
                return RedirectToAction("AddMembers");
            }
            return View();
        }
        public IActionResult Details(string id)
        {
            var user = context.Users.Include(u => u.MembershipDues).ThenInclude(md => md.MembershipType).Where(u => u.Id == id).FirstOrDefault();
            if (user == null)
            {
                TempData["MemberChanges"] = String.Format("The User with ID \"{0}\" is not a User", id);
                return RedirectToAction("Index");
            }
            MemberViewModel memberViewModel = new MemberViewModel()
            {
                id = user.Id,
                Firstname = user.UserFirstName,
                Lastname = user.UserLastName,
                Gender = user.UserGender,
                Addr1 = user.UserAddr1,
                Addr2 = user.UserAddr2,
                City = user.UserCity,
                State = user.UserState,
                PostalCode = user.UserPostalCode,
                Email = user.Email,
                EmailConfirmed = user.Email,
                Username = user.UserName,
                BirthDate = user.UserBirthDate,
                PhoneNumber = user.PhoneNumber,
            };
            if(user.MembershipDues.Count() > 0 && user.MembershipDues.Last().MemActive)
            {
                memberViewModel.MembershipType = user.MembershipDues.Last().MembershipType.Name;
                memberViewModel.MemberSince = MembershipDues.GetConsecutiveDate(user.MembershipDues.ToList());
                memberViewModel.LastRenewalDate = user.MembershipDues.Last().MemStartDate;
                memberViewModel.FutureEndDate = user.MembershipDues.Last().MemEndDate;
                memberViewModel.FutureRenewalDate = user.MembershipDues.Last().MemRenewalDate;
            }
            
            ViewBag.Action = "Details";
            return View("EditMember", memberViewModel);
        }
        public async Task<IActionResult> DonorDetails(string id)
        {
            var user = context.Users.Include(u => u.MembershipDues).Where(u => u.Id == id).FirstOrDefault();
            if (user == null)
            {
                TempData["MemberChanges"] = String.Format("The User with ID \"{0}\" is not a User", id);
                return RedirectToAction("AddMembers");
            }
            ViewBag.Action = "Details";
            return View("EditMember");
        }

        

        public IActionResult EditMember()
        {
            return View();
        }
    }
}
