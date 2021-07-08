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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvPaymentID { get; set; }
        public int ReceiptID { get; set; }
        public Receipts Receipt { get; set; }
        public string CardholderName { get; set; }
        public string CardType { get; set;}
        public int CardNumber { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime ExpDate { get; set; }
        public int CVV { get; set; }
        public int Last4Digits { get; set; }
    }
}
