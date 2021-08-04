using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NonProfitProject.Models
{
    //holds event table information
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
        [DataType(DataType.DateTime)]
        [JsonProperty(PropertyName = "StartDate")]
        public DateTime EventStartDate { get; set; }
        [Required(ErrorMessage = "Please enter an end date for the event")]
        [DataType(DataType.DateTime)]
        [JsonProperty(PropertyName = "EndDate")]
        public DateTime EventEndDate { get; set; }
        [Required(ErrorMessage = "Please enter an event description")]
        [StringLength(200)]
        [JsonProperty(PropertyName = "EventDescription")]
        public string EventDescription { get; set; }
        [Required(ErrorMessage = "Please enter event address")]
        public string EventAddr1 { get; set; }
        public string EventAddr2 { get; set; }
        [Required(ErrorMessage = "Please enter event city")]
        public string EventCity { get; set; }
        [Required(ErrorMessage = "Please enter event state")]
        public string EventState { get; set; }
        [Required(ErrorMessage = "Please enter event postal code")]
        public int EventPostalCode { get; set; }
    }
}
