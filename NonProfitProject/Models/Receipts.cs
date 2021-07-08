using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models
{
    public class Receipts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceiptID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Donations Donation { get; set; }
        public MembershipDues MembershipDue { get; set; }
        public InvoiceDonorInformation InvoiceDonorInformation { get; set; }
        public InvoicePayment InvoicePayment { get; set; }

    }
}
