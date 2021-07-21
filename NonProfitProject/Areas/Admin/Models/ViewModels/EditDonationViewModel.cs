using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Models.ViewModels
{
    public class EditDonationViewModel
    {
        //receipt information
        public int ReceiptID { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Please select donation amount")]
        public decimal DonationAmount { get; set; }

        //personal information
        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter Email Address")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        [Required(ErrorMessage = "Please enter City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter Postal Code")]
        public int PostalCode { get; set; }

        public bool isChangingCardInformation { get; set; }

        //card information
        [Required(ErrorMessage = "Please enter Cardholder name")]
        public string CardholderName { get; set; }
        [Required(ErrorMessage = "Please select Card Type")]
        public string CardType { get; set; }
        [Required(ErrorMessage = "Please enter Card Number")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Please enter Expiration Date")]
        public string ExpDate { get; set; }
        [Required(ErrorMessage = "Please enter CVV")]
        public string CVV { get; set; }

        //billing information
        [Required(ErrorMessage = "Please enter First Name")]
        public string BillingFirstName { get; set; }
        [Required(ErrorMessage = "Please enter Last Name")]
        public string BillingLastName { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        public string BillingAddr1 { get; set; }
        public string BillingAddr2 { get; set; }
        [Required(ErrorMessage = "Please enter City")]
        public string BillingCity { get; set; }
        [Required(ErrorMessage = "Please enter State")]
        public string BillingState { get; set; }
        [Required(ErrorMessage = "Please enter Postal Code")]
        public int? BillingPostalCode { get; set; }
    }
}
