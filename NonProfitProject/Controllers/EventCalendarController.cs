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
        public IActionResult Index()
        {
            var events = context.Events.ToList();
            List<CalendarViewModel> model = new List<CalendarViewModel>();
            foreach(var e in events)
            {
                model.Add(new CalendarViewModel
                {
                    EventName = e.EventName,
                    EventStartDate = e.EventStartDate,
                    EventEndDate = e.EventEndDate
                });
            }
            return View(model);
        }
    }
}

