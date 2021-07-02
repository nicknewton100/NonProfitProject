using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using NonProfitProject.Models;
using Microsoft.EntityFrameworkCore;

namespace NonProfitProject.Areas.Admin.Controllers
{

    //If user is admin, shows this page.
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private NonProfitContext context;
        public DashboardController(NonProfitContext context)
        {
            this.context = context;
        }
        //Shows Employees Table List
        public IActionResult EmployeeTable()
        {
            var employees = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).ToList();
            return View(employees);
        }

        //Shows Users Table List
        public IActionResult UserTable()
        {
            return View();
        }

        //Show Committee Members Table List
        public IActionResult CommitteeTable()
        {
            return View();
        }

        //Shows Dashboard 2
        public IActionResult Dashboard2()
        {
            var events = context.Events.ToList();
            return View(events);
        }
        //Shows Dashboard 3
        public IActionResult Dashboard3()
        {
            return View();
        }
    }
}
