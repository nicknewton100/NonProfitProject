using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Areas.Employee.Controllers.DefaultControllers;
using NonProfitProject.Areas.Employee.Data;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee")]
    [Area("Employee")]
    public class HomeController : DefaultEmployeeController
    {
        public HomeController(NonProfitContext context) : base(context){}
        //displays main dashboard
        public IActionResult Index()
        {
            return View();
        }
    }
}
