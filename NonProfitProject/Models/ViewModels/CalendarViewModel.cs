using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.ViewModels
{
    public class CalendarViewModel
    {
        public string EventName { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string EventDescription { get; set; }
        public string EventAddr1 { get; set; }
        public string EventAddr2 { get; set; }
        public string EventCity { get; set; }
        public string EventState { get; set; }
        public int EventPostalCode { get; set; }

    }
}
