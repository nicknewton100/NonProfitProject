using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.ViewModels.Shared.Users
{
    public class PaymentViewModel
    {
        public int SavedPaymentID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string CardholderName { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string CVV { get; set; }
        public int Last4Digits { get; set; }
    }
}
