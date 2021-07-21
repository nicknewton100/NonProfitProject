using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.ViewModels
{
    public class DonationPaymentViewModel
    {
        [Required(ErrorMessage = "Please enter Cardholder Name")]
        public string CardholderName { get; set; }
        [Required(ErrorMessage = "Please select Card Type")]
        public string CardType { get; set; }
        [Required(ErrorMessage = "Please enter Card number")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Please enter Card Expiration Date")]
        public string ExpDate { get; set; }
        [Required(ErrorMessage = "Please enter CVV Number")]
        public string CVV { get; set; }

        //this is used for billing address information
        [Required(ErrorMessage = "Please enter First Name")]
        public string BillingFirstName { get; set; }
        [Required(ErrorMessage = "Please enter Last Name")]
        public string BillingLastName { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        public string BillingAddr1 { get; set; }
        public string BillingAddr2 { get; set; }
        [Required(ErrorMessage = "Please enter City")]
        public string BillingCity { get; set; }
        [Required(ErrorMessage = "Please select a State")]
        public string BillingState { get; set; }
        [Required(ErrorMessage = "Please enter Zip Code")]
        public int BillingPostalCode { get; set; }

        public bool useHomeAddress { get; set; }
        public bool savePayment { get; set; }
    }
}
