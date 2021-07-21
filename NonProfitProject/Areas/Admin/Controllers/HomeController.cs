using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using NonProfitProject.Models;

namespace NonProfitProject.Areas.Admin.Controllers
{
    //If admin, show this page.
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        //Admin home page view
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {   
            return View();
        }
    }
}
