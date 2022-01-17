﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using NonProfitProject.Code;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]s/[action]/{id?}")]

    public class EventController : Controller
    {
        private NonProfitContext context;
        private IEmailManager emailManager;
        public EventController(NonProfitContext context, IEmailManager emailManager)
        {
            this.context = context;
            this.emailManager = emailManager;
        }
        //Shows all events if the date hasnt already past
        [Route("~/[area]/[controller]s")]
        public IActionResult Index()
        {
            //queries event information 
            var events = context.Events.Where(e => e.EventEndDate > DateTime.Now).OrderBy(e => e.EventStartDate).ToList();
            return View(events);
        }
        //shows add event page
        public IActionResult AddEvent()
        {
            ViewBag.Action = "Add";
            return View("EditEvent", new Event());
        }
        //shows edit event page and gets event by id 
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
        //edits or adds the event depending on if the event has a ID or not
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
        //deletes event  based on id 
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

        //allows the user to share the event via email based on what email is route to it
        [Route("~/[area]/[controller]s/{id}/{email}")]
        [HttpPost]
        public IActionResult Share(int id, string email)
        {
            var evnt = context.Events.Find(id);
            if (evnt != null)
            {
                var emailTester = new EmailAddressAttribute();
                if (!emailTester.IsValid(email))
                {
                    TempData["Email"] = "Email Address Invalid";
                    RedirectToAction("Index");
                }
                string datetime;
                string address;
                if (evnt.EventStartDate.Date == evnt.EventEndDate.Date)
                {
                    datetime = evnt.EventStartDate.ToString("D") + "   " + evnt.EventStartDate.ToString("t") + " - " + evnt.EventEndDate.ToString("t");
                }
                else
                {
                    datetime = evnt.EventStartDate.ToString("f") + " - " + evnt.EventEndDate.ToString("f");
                }
                if (evnt.EventAddr2 == null)
                {
                    address = evnt.EventAddr1 + ", " + evnt.EventCity + ", " + evnt.EventState + " " + evnt.EventPostalCode;
                }
                else
                {
                    address = evnt.EventAddr1 + " " + evnt.EventAddr2 + ", " + evnt.EventCity + ", " + evnt.EventState + " " + evnt.EventPostalCode;
                }
                //creates html email based on the event information and sends it to the email rpovided
                string html = "<h1 style = \"text-align:center;\">" + evnt.EventName + "</h1><div style = \"font-size:medium;\"><h4 style = \"margin-bottom:10px;text-align:left;\"> Details </h4><p style = \"text-align:left;\" >" + datetime + "</p><p style = \"text-align:left;\" >" + address + "</p><br/><p style = \"text-align:left;\">" + evnt.EventDescription + "</p></div>";
                emailManager.SendEmail(email, emailManager.CreateHTMLMessage(evnt.EventName, html));
            }
            return RedirectToAction("Index");
        }
    }
}
