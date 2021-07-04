using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class EventController : Controller
    {
        private NonProfitContext context;
        public EventController(NonProfitContext context)
        {
            this.context = context;
        }
        //Shows Dashboard 2
        public IActionResult Index()
        {
            //queries event information 
            var events = context.Events.ToList();
            return View(events);
        }
        public IActionResult AddEvent()
        {
            return View();
        }

    }
}
