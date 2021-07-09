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
        public IActionResult AddEmployee()
        {
            ViewBag.Action = "Add";
            return View("EditEmployee");
        }
        public IActionResult EditEmployee(string id)
        {
            ViewBag.Action = "Edit";
            var Employee = context.Employees.Include(e => e.User).FirstOrDefault(e => e.EmpID == id);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                Id = id,
                Firstname = Employee.User.UserFirstName,
                Lastname = Employee.User.UserLastName,
                Gender = Employee.User.UserGender,
                Position = Employee.Position,
                Salary = Employee.Salary,
                Addr1 = Employee.User.UserAddr1,
                Addr2 = Employee.User.UserAddr2,
                City = Employee.User.UserCity,
                State = Employee.User.UserState,
                PostalCode = Employee.User.UserPostalCode,
                Country = Employee.User.UserCountry,
                BirthDate = Employee.User.UserBirthDate,
                PhoneNumber = Employee.User.PhoneNumber,
                Username = Employee.User.UserName,
                Email = Employee.User.Email
            };
            return View(employeeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel model)
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
                var employee = new Employees
                {
                    UserID = await userManager.GetUserIdAsync(user),
                    Position = model.Position,
                    Salary = model.Salary
                };

                if(model.Id == "" || model.Id == null)
                {
                    var result = await userManager.CreateAsync(user, model.TemporaryPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Employee");
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
                else
                {
                    context.Employees.Update(employee);
                    var hasher = userManager.PasswordHasher;
                    user.PasswordHash = hasher.HashPassword(user, model.TemporaryPassword);
                    await userManager.UpdateAsync(user);
                    context.SaveChanges();
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployeeAsync(string id)
        {
            var employee = context.Employees.Include(e => e.User).FirstOrDefault(e => e.EmpID == id);
            context.Employees.Remove(employee);
            var result = await userManager.DeleteAsync(employee.User);
            if (result.Succeeded)
            {
                context.SaveChanges();
                TempData["EmployeeChanges"] = String.Format("{0} has been deleted", employee.User.UserFirstName + " " + employee.User.UserLastName);
                return RedirectToAction("Index");
            }
            TempData["EmployeeChanges"] = String.Format("An error has occured while trying to delete {0} from the database", employee.User.UserFirstName + " " + employee.User.UserLastName);
            return View();
            
        }

    }
}
