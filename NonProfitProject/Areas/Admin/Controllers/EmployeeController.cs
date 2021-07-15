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
    public class EmployeeController : Controller
    {
        private NonProfitContext context;
        private UserManager<User> userManager;

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
            return View("EditEmployee", new EmployeeViewModel { BirthDate = null, AllCommittees = new SelectList(context.Committees.Select(c => c.CommitteeName).ToList(), "CommitteeName") });
        }
        public IActionResult EditEmployee(string id)
        {
            TempData["Action"] = "Edit";
            var Employee = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).FirstOrDefault(e => e.EmpID == id);
            if(Employee == null)
            {
                return RedirectToAction("Index");
            }
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
                CommitteeName = context.Committees?.Where(c => c.CommitteesID == (Employee.CommitteeMembers == null ? 0 : (Employee.CommitteeMembers.CommitteeID))).Select(c => c.CommitteeName).FirstOrDefault() ?? "",
                CommitteePosition = Employee.CommitteeMembers?.CommitteePosition ?? "",
                AllCommittees = new SelectList(context.Committees.Select(c => c.CommitteeName).ToList(), "CommitteeName")
            };
            return View(employeeViewModel);
        }
        public IActionResult Details(string id)
        {
            TempData["Action"] = "Details";
            var Employee = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).FirstOrDefault(e => e.EmpID == id);
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
                HireDate = Employee.HireDate,
                ReleaseDate = Employee.ReleaseDate,
                CommitteeName = context.Committees?.Where(c => c.CommitteesID == (Employee.CommitteeMembers == null ? 0 : (Employee.CommitteeMembers.CommitteeID))).Select(c => c.CommitteeName).FirstOrDefault() ?? "",
                CommitteePosition = Employee.CommitteeMembers?.CommitteePosition ?? "",
                AllCommittees = new SelectList(context.Committees.Select(c => c.CommitteeName).ToList(), "CommitteeName") 
            };
            return View("EditEmployee",employeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel model)
        {
            model.AllCommittees = new SelectList(context.Committees.Select(c => c.CommitteeName).ToList(), "CommitteeName");
            var committiees = context.Committees.Select(c => c.CommitteeName).ToList();
            foreach (string C in committiees)
            {
                if (!C.Equals(model.CommitteeName) && committiees.Last().Equals(C))
                {
                    ModelState.AddModelError("AllCommittees", String.Format("The Committee {0} does not exist", model.CommitteeName));
                }
                else if(C.Equals(model.CommitteeName))
                {
                    break;
                }
            }
            if (TempData["Action"].ToString().Equals("Edit") && model.Id != null)
            {
                return await EditEmployee(model);
            }

            if (context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("", String.Format("The email address {0} is already in use", model.Email));
            }
            if (ModelState.IsValid)
            {
                User user = new User
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
                    UserCountry = model.Country,
                    PhoneNumber = model.PhoneNumber,
                    UserCreationDate = DateTime.UtcNow,
                    UserLastActivity = DateTime.UtcNow,
                    ReceiveWeeklyNewsletter = false,
                    AccountDisabled = false  
                };
                Employees employee = new Employees 
                {
                    UserID = await userManager.GetUserIdAsync(user),
                    Position = model.Position,
                    Salary = (decimal)model.Salary,
                    HireDate = DateTime.UtcNow,
                    ReleaseDate = null
                };

                var result = await userManager.CreateAsync(user, model.TemporaryPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    if (!model.CommitteeName.Equals("None"))
                    {
                        AddToCommittee(model);
                    }
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
            return View("EditEmployee",model);
        }

        //allows the admin to edit employee information and if the admin chooses to check the "change login information" check box, the Admin will change the login information
        //if he doesnt, it will use the information that was already saved before hand
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel model)
        {
            User user;
            Employees employee;
            //this try catch makes sure that the user and employee exists before trying to chnage them. If tehy dont, it sends it back to add employee
            //this was done because the data on the website could potentially be altered which could cause an error or the id sent through the url could be altered as well
            try
            {
                employee = context.Employees.Include(e => e.User).FirstOrDefault(e => e.EmpID == model.Id);
                user = employee.User;
            }
            catch (Exception e)
            {
                model.Id = null;
                return await AddEmployee(model);
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
                user.UserCountry = model.Country;
                user.PhoneNumber = model.PhoneNumber;
                user.UserLastActivity = DateTime.UtcNow;

                employee.UserID = await userManager.GetUserIdAsync(user);
                employee.Position = model.Position;
                employee.Salary = (decimal)model.Salary;
                context.Employees.Update(employee);

                if(model.TemporaryPassword != null && model.IsChangingLogininformation)
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
                    employee.FinishedAccountSetup = false;
                }
                var updateUser = await userManager.UpdateAsync(user);
                if (updateUser.Succeeded)
                {
                    context.SaveChanges();
                    if (!model.CommitteeName.Equals("None"))
                    {
                        AddToCommittee(model);
                    }
                    TempData["EmployeeChanges"] = String.Format("{0} has been updated", model.Firstname + " " + model.Lastname);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in updateUser.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View("EditEmployee", model);
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

        public void AddToCommittee(EmployeeViewModel model)
        {        
            CommitteeMembers committeeMember = context.CommitteeMembers?.Where(cm => cm.EmpID == model.Id).FirstOrDefault()  ?? null;
            if (committeeMember == null)
            {
                committeeMember = new CommitteeMembers()
                {
                    CommitteeID = context.Committees.Where(c => c.CommitteeName == model.CommitteeName).Select(c => c.CommitteesID).FirstOrDefault(),
                    EmpID = context.Employees.Where(e => e.User.UserName == model.Username).Select(e => e.EmpID).FirstOrDefault(),
                    CommitteePosition = model.CommitteePosition
                };
                context.CommitteeMembers.Add(committeeMember);
            }
            else
            {
                committeeMember.CommitteeID = context.Committees.Where(c => c.CommitteeName == model.CommitteeName).Select(c => c.CommitteesID).FirstOrDefault();
                committeeMember.CommitteePosition = model.CommitteePosition;
                context.CommitteeMembers.Update(committeeMember);
            }
            context.SaveChanges();
        }
    }
}
