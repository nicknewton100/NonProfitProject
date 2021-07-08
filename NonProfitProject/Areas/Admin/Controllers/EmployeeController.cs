using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class EmployeeController : Controller
    {
        private NonProfitContext context;
        private UserManager<User> userManager;
        private SignInManager<User> SignInManager;

        public EmployeeController(NonProfitContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        [Route("~/[area]/[controller]s")]
        public IActionResult Index()
        {
            //queries employee information
            var employees = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).ToList();
            return View(employees);
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel model)
        {
            if (context.Users.Any(u => u.Email == model.Email))
            {
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
                    UserCountry = model.Country,
                    PhoneNumber = model.PhoneNumber

                };
                var result = await userManager.CreateAsync(user, model.TemporaryPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                    var employee = new Employees
                    {
                        UserID = await userManager.GetUserIdAsync(user),
                        Position = model.Position,
                        Salary = model.Salary
                    };
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    TempData["EmployeeChanges"] = String.Format("{0} has been added as an employee", model.Firstname + " " + model.Lastname);
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

    }
}
