using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NonProfitProject.Models
{
    public class News
    {
        public int NewsID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsHeader { get; set; }
        public string NewsFooter { get; set; }
        public DateTime NewsCreationDate { get; set; }
        public DateTime NewsPublishDate { get; set; }
        public DateTime NewsLastUpdated { get; set; }
    }
}
