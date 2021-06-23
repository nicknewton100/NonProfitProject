using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace NonProfitProject.Areas.Admin.Controllers
{

    //If user is admin, shows this page.
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        //Shows Dashboard 1
        public IActionResult Dashboard1()
        {
            return View();
        }
        //Shows Dashboard 2
        public IActionResult Dashboard2()
        {
            return View();
        }
        //Shows Dashboard 3
        public IActionResult Dashboard3()
        {
            return View();
        }
    }
}
