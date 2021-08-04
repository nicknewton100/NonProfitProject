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
        [Route("~/[area]/[controller]s")]
        public async Task<IActionResult> Index()
        {
            var member = await userManager.GetUsersInRoleAsync("Member");
            var members = context.Users.Where(u => u.UserName != "One-TimeDonation" && member.Contains(u)).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).Include(u => u.MembershipDues).ToList();
            return View(members);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMember(string id)
        {
            var user = context.Users.Include(u => u.MembershipDues).Where(u => u.Id == id).FirstOrDefault();
            if (user == null)
            {
                TempData["MemberChanges"] = String.Format("The UserID \"{0}\" does not exist", id);
                return RedirectToAction("Index");
            }
            else
            {
                if(user.MembershipDues != null)
                {
                    user.MembershipDues.Last().MemCancelDate = DateTime.UtcNow;
                    user.MembershipDues.Last().MemActive = false;
                    context.MembershipDues.Update(user.MembershipDues.Last());
                    context.SaveChanges();
                }
                
                TempData["MemberChanges"] = String.Format("{0} is no longer a member", user.UserFirstName + " " + user.UserLastName);
                var result = await userManager.RemoveFromRoleAsync(user, "Member");
                if(!await userManager.IsInRoleAsync(user,"Admin"))
                {
                    await userManager.AddToRoleAsync(user, "User");
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


        public IActionResult DonorDetails(string id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                TempData["MemberChanges"] = String.Format("The User with ID \"{0}\" is not a User", id);
                RedirectToAction("AddMembers");
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

        public IActionResult Details(string id)
        {
            TempData.Clear();
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
                TempData["Membership"] = "Membership";
            }

            ViewBag.Action = "Details";
            return View("EditMember", memberViewModel);
        }
        

        [HttpGet]
        public IActionResult EditMember(string id)
        {
            TempData.Clear();
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
            if (user.MembershipDues.Count() > 0 && user.MembershipDues.Last().MemActive)
            {
                memberViewModel.MembershipType = user.MembershipDues.Last().MembershipType.Name;
                memberViewModel.MemberSince = MembershipDues.GetConsecutiveDate(user.MembershipDues.ToList());
                memberViewModel.LastRenewalDate = user.MembershipDues.Last().MemStartDate;
                memberViewModel.FutureEndDate = user.MembershipDues.Last().MemEndDate;
                memberViewModel.FutureRenewalDate = user.MembershipDues.Last().MemRenewalDate;
                TempData["Membership"] = "Membership";
            }

            ViewBag.Action = "Edit";
            
            
            return View(memberViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditMember(MemberViewModel model)
        {
            var user = context.Users.Include(u => u.MembershipDues).Where(u => u.Id == model.id).FirstOrDefault();
            if(user == null)
            {
                return RedirectToAction("Index");
            }
            if(string.IsNullOrWhiteSpace(model.MembershipType))
            {
                if(TempData["Membership"]?.ToString() == "Membership")
                {
                    TempData.Clear();
                    user.MembershipDues.Last().MemActive = false;
                    user.MembershipDues.Last().MemCancelDate = DateTime.UtcNow;
                }
                ModelState.Remove("MembershipType");
                ModelState.Remove("LastRenewalDate");
                ModelState.Remove("FutureEndDate");
                ModelState.Remove("FutureRenewalDate");

            }
            else if(user.MembershipDues.Last() != null && user.MembershipDues.Last().MemActive && model.FutureRenewalDate != null && model.FutureEndDate != null)
            {
                ViewBag.Membership = "Membership";
                if(model.FutureEndDate > model.FutureRenewalDate)
                {
                    ModelState.AddModelError("FutureEndDate", "End Date has to be before Renewal Date");
                }
                else if (model.FutureEndDate < DateTime.UtcNow.Date)
                {
                    ModelState.AddModelError("FutureEndDate", "End Date and Renewal Date have to be in the future");
                }
                else
                {
                    user.MembershipDues.Last().MembershipTypeID = context.MembershipTypes.Where(mt => mt.Name == model.MembershipType).Select(mt => mt.MembershipTypeID).FirstOrDefault();
                    user.MembershipDues.Last().MemRenewalDate = (DateTime)model.FutureRenewalDate;
                    user.MembershipDues.Last().MemEndDate = (DateTime)model.FutureEndDate;
                    ModelState.Remove("MembershipType");
                    ModelState.Remove("LastRenewalDate");
                    ModelState.Remove("FutureEndDate");
                    ModelState.Remove("FutureRenewalDate");
                }
                
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
                ViewBag.Action = "Details";
                return RedirectToAction("Details", new { id = model.id });
            }
            ViewBag.Action = "Edit";
            return View();
        }
    }
}
