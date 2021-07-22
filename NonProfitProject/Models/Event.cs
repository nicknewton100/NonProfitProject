using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NonProfitProject.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "EventID")]
        public int EventID { get; set; }
        [Required(ErrorMessage = "Please enter an event name")]
        [StringLength(100)]
        [JsonProperty(PropertyName = "EventName")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Please enter a start date for the event")]
        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "StartDate")]
        public DateTime EventStartDate { get; set; }
        [Required(ErrorMessage = "Please enter an end date for the event")]
        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "EndDate")]
        public DateTime EventEndDate { get; set; }
        [Required(ErrorMessage = "Please enter an event description")]
        [StringLength(200)]
        [JsonProperty(PropertyName = "EventDescription")]
        public string EventDescription { get; set; }
    }
}
