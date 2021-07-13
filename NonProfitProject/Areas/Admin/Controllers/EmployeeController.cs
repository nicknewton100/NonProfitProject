using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            TempData["Action"] = "Add";
            return View("EditEmployee");
        }
        public IActionResult EditEmployee(string id)
        {
            TempData["Action"] = "Edit";
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
                Email = Employee.User.Email,
                EmailConfirmed = Employee.User.Email,
                UserID = Employee.User.Id
            };
            return View(employeeViewModel);
        }

        //allows the user to add or edit employee depending on if the employee has an ID or not. 
        //It allows the option to choose if the admin wants to change the password or not and uses the editemployee view for both adding and editing
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel model)
        {
            /////////This method is very unorganized but it works. I(Nick) will go back and reorganize it once ive distanced myself from this for a while
            if (TempData["Action"].ToString().Equals("Edit"))
            {
                var Employee = context.Employees.Include(e => e.User).AsNoTracking().FirstOrDefault(e => e.EmpID == model.Id);
                
                if (!model.IsChangingLogininformation)
                {
                    model.TemporaryPassword = Employee.User.PasswordHash;
                }
                if(model.TemporaryPassword == null)
                {
                    ModelState.Remove("TemporaryPassword");
                    ModelState.Remove("TemporaryPasswordConfirmed");
                }
                
                context.Entry(Employee).State = EntityState.Detached;
            }
            else
            {
                if (context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", String.Format("The email address {0} is already in use", model.Email));
                }
            }
            if (ModelState.IsValid)
            {
                User user;
                Employees employee;
                if (model.UserID != null)
                {
                    user = await userManager.FindByIdAsync(model.UserID);
                    employee = context.Employees.FirstOrDefault(e => e.EmpID == model.Id);
                }
                else
                {
                    user = new User();
                    employee = new NonProfitProject.Models.Employees();
                }
                user.UserName = model.Username;
                user.Email = model.Email;
                user.UserFirstName = model.Firstname;
                user.UserLastName = model.Lastname;
                user.UserGender = model.Gender;
                user.UserBirthDate = (DateTime)model.BirthDate;
                user.UserAddr1 = model.Addr1;
                user.UserAddr2 = model.Addr2;
                user.UserCity = model.City;
                user.UserState = model.State;
                user.UserPostalCode = (int)model.PostalCode;
                user.UserCountry = model.Country;
                user.PhoneNumber = model.PhoneNumber;

                employee.UserID = await userManager.GetUserIdAsync(user);
                employee.Position = model.Position;
                employee.Salary = (decimal)model.Salary;

                if(model.Id == null)
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
                    if (model.TemporaryPassword == null || model.IsChangingLogininformation == false)
                    {
                        user.PasswordHash = model.TemporaryPassword;
                    }
                    else
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);
                        var passwordChange = await userManager.ResetPasswordAsync(user, token, model.TemporaryPassword);
                        if (!passwordChange.Succeeded)
                        {
                            foreach (IdentityError i in passwordChange.Errors)
                            {
                                ModelState.AddModelError("TemporaryPassword", i.Description);
                            }
                            return View();
                        }
                        employee.FinishedAccountSetup = false;
                    }
                    var result = await userManager.UpdateAsync(user);
                    context.SaveChanges();
                    TempData["EmployeeChanges"] = String.Format("{0} has been updated", model.Firstname + " " + model.Lastname);
                    return RedirectToAction("Index");
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
