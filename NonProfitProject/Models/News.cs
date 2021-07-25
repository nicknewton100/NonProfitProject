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
        [Required(ErrorMessage ="Title Text is Requied")]
        public string NewsTitle { get; set; }
        [Required(ErrorMessage = "Header Text is Requied")]
        public string NewsHeader { get; set; }
        [Required(ErrorMessage = "Body Text is Requied")]
        [DataType(DataType.MultilineText)]
        public string NewsBody { get; set; }
        public DateTime NewsPublishDate { get; set; }
        public DateTime NewsLastUpdated { get; set; }
        public string CreatedBy { get; set; }
        
    }
}
