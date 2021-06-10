using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NonProfitProject.Models
{
    public class PaymentInformation
    {
        [Key]
        public int PaymentID { get; set; }
        public string UserID { get; set; }
        public User user { get; set; }
        public string CardholderName { get; set; }
        public string CardType { get; set; }
        
        [CreditCard]
        public string CardNo { get; set; }
        public DateTime ExpDate { get; set; }
        public int CVV { get; set; }

        public ICollection<DonationRecipts> donationRecipts { get; set; }
    }
}
