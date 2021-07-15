using Microsoft.AspNetCore.Authorization;
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
    public class UserController : Controller
    {
        private NonProfitContext context;
        public UserController(NonProfitContext context)
        {
            this.context = context;
        }
        //Shows Users Table List
        [Route("~/[area]/[controller]s")]
        public IActionResult Index()
        {
            //queries users that are not in the employee table
            var users = context.Users.Where(u => !context.Employees.Any(e => u.Id == e.UserID)).ToList();
            return View(users);
        }
        public IActionResult AddUser()
        {
            return View();
        }

        //////////////////////////////////////////////////////////////////////
        //Edit 2 pieces of code below to appropriate where to return view from
        [HttpGet]
        public IActionResult EditUser(int userID)
        {
            ViewBag.Action = "Edit";
            var User = context.Users.Find(userID);
            return View(User);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
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
