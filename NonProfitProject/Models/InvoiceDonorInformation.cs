using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models
{
    public class InvoiceDonorInformation
    {
        //holds invoice donor information. This is done to allow the users to delete saved information or even their profile but holds the information for later use. Also allows non members to donate
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorInfoID { get; set; }
        public int ReceiptID { get; set; }
        public Receipts Receipt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
    }
}
