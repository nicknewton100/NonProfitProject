using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NonProfitProject.Models
{
    public class Donations
    {
        [Key]
        public string DonationID { get; set; }
        public decimal DonationAmount { get; set; }
        public DateTime DonationDate { get; set; }
        public string Comments { get; set; }

        public DonationRecipts donationRecipts { get; set; }
    }
}
