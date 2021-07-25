using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    public class MembershipHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
