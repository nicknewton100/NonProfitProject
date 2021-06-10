using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NonProfitProject.Models
{
    public class MembershipDues
    {
        [Key]
        public string MemDuesID { get; set; }
        public decimal MemDueAmount { get; set; }
        public DateTime MemStartDate { get; set; }
        public DateTime MemEndDate { get; set; }
        public DateTime MemRenewalDate { get; set; }
        public char MemActive { get; set; }


        public DonationRecipts donationRecipts { get; set; }
    }
}
