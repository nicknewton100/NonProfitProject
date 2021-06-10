using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NonProfitProject.Models
{
    public class DonationRecipts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonationRecId { get; set; }
        public string UserID { get; set; }
        public User user { get; set; }
        public int PaymentID { get; set; }
        public PaymentInformation paymentInformation { get; set; }
        public string ReciptDonationID { get; set; }
        public Donations donations { get; set; }
        public MembershipDues membershipDues { get; set; }
        public string ReciptDesc { get; set; }
        
        
        
    }
}
