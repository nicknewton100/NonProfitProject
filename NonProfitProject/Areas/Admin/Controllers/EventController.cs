using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]s/[action]/{id?}")]

    public class EventController : Controller
    {
        private NonProfitContext context;
        public EventController(NonProfitContext context)
        {
            this.context = context;
        }
        //Shows Events
        [Route("~/[area]/[controller]s")]
        public IActionResult Index()
        {
            //queries event information 
            var events = context.Events.OrderBy(e => e.EventStartDate).ToList();
            return View(events);
        }
        public IActionResult AddEvent()
        {
            ViewBag.Action = "Add";
            return View("EditEvent", new Event());
        }
        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            ViewBag.Action = "Edit";
            var Event = context.Events.Find(id);
            if (Event == null)
            {
                return RedirectToAction("Index");
            }
            return View(Event);
        }
        [HttpPost]
        public IActionResult EditEvent(Event model)
        {
            if(model.EventStartDate >= model.EventEndDate)
            {
                ModelState.AddModelError("EventStartDate", "Event Start Date must be before End Date");
            }
            
            if (ModelState.IsValid)
            {
                string addOrEdit;
                if (model.EventID == 0 || context.Events.AsNoTracking().Where(e => e.EventID == model.EventID).FirstOrDefault() == null)
                {
                    model.EventID = 0;
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
            if(Event == null)
            {
                TempData["EventChanges"] = String.Format("The Event with EventID \"{0}\" does not exist", id);
            }
            else
            {
                context.Remove(Event);
                context.SaveChanges();
                TempData["EventChanges"] = String.Format("The Event \"{0}\" has been deleted", Event.EventName);
            }
            
            return RedirectToAction("Index");
        }
    }
}
