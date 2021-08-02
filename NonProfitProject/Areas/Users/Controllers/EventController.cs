using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NonProfitProject.Code;

namespace NonProfitProject.Areas.Users.Controllers
{
    [Authorize(Roles = "User,Member")]
    [Area("Users")]
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
        [Route("~/[area]/[controller]s/{id}/{email}")]
        [HttpPost]
        public IActionResult Share(int id, string email)
        {
            var evnt = context.Events.Find(id);
            if(evnt != null)
            {
                var emailTester = new EmailAddressAttribute();
                if (!emailTester.IsValid(email))
                {
                    TempData["Email"] = "Email Address Invalid";
                    RedirectToAction("Index");
                }
                EmailManager emailManager = new EmailManager(context);
                string datetime;
                string address;
                if(evnt.EventStartDate.Date == evnt.EventEndDate.Date)
                {
                    datetime = evnt.EventStartDate.ToString("D") + "   " + evnt.EventStartDate.ToString("t") + " - " + evnt.EventEndDate.ToString("t");
                }
                else
                {
                    datetime = evnt.EventStartDate.ToString("f") + " - " + evnt.EventEndDate.ToString("f");
                }
                if(evnt.EventAddr2 == null)
                {
                    address = evnt.EventAddr1 + ", " + evnt.EventCity + ", " + evnt.EventState + " " + evnt.EventPostalCode;
                }
                else
                {
                    address = evnt.EventAddr1 + " " + evnt.EventAddr2 + ", " + evnt.EventCity + ", " + evnt.EventState + " " + evnt.EventPostalCode;
                }
                string html = "<h1 style = \"text-align:center;\">" + evnt.EventName +"</h1><div style = \"font-size:medium;\"><h4 style = \"margin-bottom:10px;text-align:left;\"> Details </h4><p style = \"text-align:left;\" >" + datetime + "</p><p style = \"text-align:left;\" >" + address + "</p><br/><p style = \"text-align:left;\">" + evnt.EventDescription + "</p></div>";
                emailManager.SendEmail(email,emailManager.CreateHTMLMessage(evnt.EventName, html));
            }
            return RedirectToAction("Index");
        }
    }
}
