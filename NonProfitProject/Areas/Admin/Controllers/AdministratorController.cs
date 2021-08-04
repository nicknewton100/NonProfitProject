using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AdministratorController : Controller
    {
        private NonProfitContext context;
        private UserManager<User> userManager;
        public AdministratorController(NonProfitContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        //shows index page for Administrative users. Queries all employees that are in the Admin role
        [Route("~/[area]/[controller]s")]
        public async Task<IActionResult> Index()
        {
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            var users = context.Employees.Include(e => e.User).Where(e => admins.Contains(e.User)).ToList();
            return View(users);
        }
        //Removes administrator from the admin role if the employee exists
        [HttpPost]
        public async Task<IActionResult> RemoveAdministrator(string id)
        {
            var employee = context.Employees.Include(e => e.User).Where(u => u.EmpID == id).FirstOrDefault();

            if (employee == null)
            {
                TempData["AdminChanges"] = String.Format("The Employee ID \"{0}\" does not exist", id);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AdminChanges"] = String.Format("{0} is no longer an Administrator", employee.User.UserFirstName + " " + employee.User.UserLastName);
                var result = await userManager.RemoveFromRoleAsync(employee.User, "Admin");
            }
            return RedirectToAction("Index");
        }
        //displays employees who could be added to the administrative role if their account is not disabled(terminated)
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            var employees = context.Employees.Include(e => e.User).Where(e => !admins.Contains(e.User) && e.User.AccountDisabled == false).ToList();
            return View(employees);
        }
        //adds employee to admini role if the user exists
        [HttpPost]
        public async Task<IActionResult> Add(string id)
        {
            var employee = context.Employees.Include(e => e.User).Where(e => e.EmpID == id && e.User.AccountDisabled == false).FirstOrDefault();
            if (employee == null || await userManager?.IsInRoleAsync(employee.User, "User"))
            {
                TempData["AdminChanges"] = String.Format("The Employee with ID \"{0}\" does not exist", id);
                return RedirectToAction("Add");
            }
            await userManager.AddToRoleAsync(employee.User, "Admin");
            TempData["AdminChanges"] = String.Format("{0} is now an Administrator", employee.User.UserFirstName + " " + employee.User.UserLastName);
            return RedirectToAction("Add");
        }
        //displays employee details of the people who are not in the admin role
        public IActionResult EmployeeDetails(string id)
        {
            var employee = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).ThenInclude(cm => cm.committee).Where(e => e.EmpID == id).FirstOrDefault();
            if (employee == null)
            {
                TempData["AdminChanges"] = String.Format("The Employee with ID \"{0}\" does not exist", id);
                RedirectToAction("Add");
            }
            return View(employee);
        }
        //displays details of the admin employees
        public async Task<IActionResult> Details(string id)
        {
            var employee = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).ThenInclude(cm => cm.committee).Where(e => e.EmpID == id).FirstOrDefault();
            var userInAdmin = await userManager.IsInRoleAsync(employee.User, "Admin");
            if (employee == null || !userInAdmin)
            {
                TempData["AdminChanges"] = String.Format("The Administrator with ID \"{0}\" does not exist", id);
                RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}
