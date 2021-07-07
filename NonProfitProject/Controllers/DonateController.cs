using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Controllers
{
    public class DonateController : Controller
    {
        public IActionResult Donate()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
    }
}
