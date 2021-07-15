using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models
{
    public class InvoicePayment
    {
        //all sensitive data will be stored as encrypted values with datatype of string
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvPaymentID { get; set; }
        public int ReceiptID { get; set; }
        public Receipts Receipt { get; set; }
        public string CardholderName { get; set; }
        public string CardType { get; set;}
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string CVV { get; set; }
        public int Last4Digits { get; set; }
    }
}
