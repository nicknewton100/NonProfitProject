using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models;
using NonProfitProject.Models.ViewModels;

namespace NonProfitProject.Controllers
{
    public class EventCalendarController : Controller
    {
        private NonProfitContext context;

        public EventCalendarController(NonProfitContext context)
        {
            this.context = context;
        }

        //displays events on event calendar. replaces any new line characters in the event description because JSON doesnt like them
        public IActionResult Index()
        {
            var events = context.Events.ToList();
            for(int i = 0; i < events.Count(); i++)
            {
                var replacedNewLine = events[i].EventDescription.Replace(System.Environment.NewLine, " ");
                events[i].EventDescription = replacedNewLine;
            }
            return View(events);
        }
    }
}

