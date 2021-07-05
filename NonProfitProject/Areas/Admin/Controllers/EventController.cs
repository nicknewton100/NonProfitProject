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
            var events = context.Events.OrderBy(e => e.EventStartDate).ToList();
            return View(events);
        }
        public IActionResult AddEvent()
        {
            ViewBag.Action = "Add";
            return View("EditEvent");
        }
        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            ViewBag.Action = "Edit";
            var Event = context.Events.Find(id);
            return View(Event);
        }
        [HttpPost]
        public IActionResult EditEvent(Event model)
        {
            if (ModelState.IsValid)
            {
                string addOrEdit;
                if (model.EventID == 0)
                {
                    context.Events.Add(model);
                    addOrEdit = "added";
                }
                else
                {
                    context.Events.Update(model);
                    addOrEdit = "updated";
                }

                context.SaveChanges();
                TempData["EventChanges"] = String.Format("The Event \"{0}\" has been {1}.", model.EventName, addOrEdit);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Action = (model.EventID == 0) ? "Add" : "Edit";
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Event = context.Events.Find(id);
            context.Remove(Event);
            context.SaveChanges();
            TempData["EventChanges"] = String.Format("The Event \"{0}\" has been deleted", Event.EventName);
            return RedirectToAction("Index");
        }
    }
}
