﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Users.Controllers
{
    [Authorize(Roles = "User,Member")]
    [Area("Users")]
    public class HomeController : Controller
    {
        //displays user dashboard
        public IActionResult Index()
        {
            return View();
        }
    }
}
