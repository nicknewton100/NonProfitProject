using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models
{
    public class MembershipDues
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemDuesID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public int ReceiptID { get; set; }
        public Receipts Receipt { get; set; }
        public string MembershipTypeID { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime MemStartDate { get; set; }
        public DateTime MemEndDate { get; set; }
        public DateTime MemRenewalDate { get; set; }
        public bool MemActive { get; set; }
    }
}
