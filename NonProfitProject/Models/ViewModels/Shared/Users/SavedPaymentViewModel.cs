using NonProfitProject.Code.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.ViewModels.Shared.Users
{
    public class SavedPaymentViewModel
    {
        //holds data and validation for account editing of payments

        [Required(ErrorMessage = "Please enter Cardholder Name")]
        public string CardholderName { get; set; }
        [Required(ErrorMessage = "Please select a Card type")]
        public string CardType { get; set; }
        [Required(ErrorMessage = "Please enter Card Number")]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Please enter Expiration Date")]
        public string CardExpDate { get; set; }
        [Required(ErrorMessage = "Please enter CVV number")]
        public string CardCVV { get; set; }


        [Required(ErrorMessage = "Please enter First name")]
        public string BillingFirstName { get; set; }
        [Required(ErrorMessage = "Please enter Last name")]
        public string BillingLastName { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        public string BillingAddr1 { get; set; }
        public string BillingAddr2 { get; set; }
        [Required(ErrorMessage = "Please enter a City")]
        public string BillingCity { get; set; }
        [Required(ErrorMessage = "Please choose a State", AllowEmptyStrings = false)]
        public string BillingState { get; set; }
        [Required(ErrorMessage = "Please enter a Postal Code")]
        [DataType(DataType.PostalCode)]
        public int? BillingPostalCode { get; set; }


        //these attributes are used to store accountAddress
        public string AcctFirstName { get; set; }
        public string AcctLastName { get; set; }
        public string AcctAddr1 { get; set; }
        public string AcctAddr2 { get; set; }
        public string AcctCity { get; set; }
        public string AcctState { get; set; }
        public int AcctPostalCode { get; set; }

        public bool usingAccountAddress { get; set; }
    }
}
