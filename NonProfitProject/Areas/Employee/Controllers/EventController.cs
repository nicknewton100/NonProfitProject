using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Employee.Controllers.DefaultControllers;
using NonProfitProject.Areas.Employee.Data;
using System.Security.Claims;
using NonProfitProject.Code;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace NonProfitProject.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee")]
    [Area("Employee")]
    [Route("[area]/[controller]s/[action]/{id?}")]

    public class EventController : DefaultEmployeeController
    {
        private NonProfitContext context;
        private IEmailManager emailManager;
        public EventController(NonProfitContext context, IEmailManager emailManager) : base(context)
        {
            this.context = context;
            this.emailManager = emailManager;
        }
        //checks to see if user is in event committee
        public bool isEventCommittee()
        {
            var name = CommitteeStatus.GetName(context, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (name == "Event and Planning Committee")
            {
                return true;
            }
            return false;
        }
        //Shows Events
        [Route("~/[area]/[controller]s")]
        //shows events
        public IActionResult Index()
        {
            //queries event information 
            var events = context.Events.Where(e => e.EventEndDate > DateTime.Now).OrderBy(e => e.EventStartDate).ToList();
            return View(events);
        }
        //shows add event if the employee is in event committee
        public IActionResult AddEvent()
        {
            if (!isEventCommittee())
            {
                return RedirectToAction("Index");
            }
            ViewBag.Action = "Add";
            return View("EditEvent", new Event());
        }
        //displays edit event based on id if the employee is in the event committee
        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            if (!isEventCommittee())
            {
                return RedirectToAction("Index");
            }
            ViewBag.Action = "Edit";
            var Event = context.Events.Find(id);
            if (Event == null)
            {
                return RedirectToAction("Index");
            }
            return View(Event);
        }
        //edits the event if the employee is in the event committee based on the model
        [HttpPost]
        public IActionResult EditEvent(Event model)
        {
            if (!isEventCommittee())
            {
                return RedirectToAction("Index");
            }
            if (model.EventStartDate >= model.EventEndDate)
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
        //delets the event based on the id
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!isEventCommittee())
            {
                return RedirectToAction("Index");
            }
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

        //shares the event based on email and id of event
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
                //creates html message and sends the email
                string html = "<h1 style = \"text-align:center;\">" + evnt.EventName + "</h1><div style = \"font-size:medium;\"><h4 style = \"margin-bottom:10px;text-align:left;\"> Details </h4><p style = \"text-align:left;\" >" + datetime + "</p><p style = \"text-align:left;\" >" + address + "</p><br/><p style = \"text-align:left;\">" + evnt.EventDescription + "</p></div>";
                emailManager.SendEmail(email, emailManager.CreateHTMLMessage(evnt.EventName, html));
            }
            return RedirectToAction("Index");
        }
    }
}
