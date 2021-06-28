using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NonProfitProject.Models
{
    public class PaymentInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }
        public string UserID { get; set; }
        public User user { get; set; }
        public string CardholderName { get; set; }
        public string CardType { get; set; }
        
        [CreditCard]
        public string CardNo { get; set; }
        public DateTime ExpDate { get; set; }
        public int CVV { get; set; }

        public ICollection<DonationReceipts> donationRecipts { get; set; }
    }
}
