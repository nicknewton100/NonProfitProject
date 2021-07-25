using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.ViewModels
{
    public class DonationViewModel
    {
        //donation information
        [Required(ErrorMessage = "Please select a Donation Amount")]
        public decimal DonationAmount { get; set; }
        [StringLength(160)]
        public string Comments { get; set; }
        //personal information 
        [Required(ErrorMessage = "Please provide your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide your Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please provide your Email Address")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please provide your Address")]
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        [Required(ErrorMessage = "Please provide your City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please provide your State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please provide your Postal Code")]
        public int PostalCode { get; set; }

        public DonationPaymentViewModel DonationPaymentViewModel { get; set; }
    }
}
