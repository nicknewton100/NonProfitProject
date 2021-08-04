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
        public DateTime? MemCancelDate { get; set; }

        //calculates how long a user has been paying for a membership consecutivly

        public static DateTime? GetConsecutiveDate(List<MembershipDues> membershipDues)
        {
            if(membershipDues.Count == 0)
            {
                return null;
            }
            membershipDues.OrderBy(md => new { md.MemStartDate, md.MemRenewalDate });
            DateTime? consecutiveMember = null;
            if (membershipDues.Last().MemActive)
            {
                string membershipType = membershipDues.Last().MembershipType.Name;
                for (int i = membershipDues.Count - 1; i >= 0; i--)
                {
                    if(i == 0 || membershipDues[i].MemStartDate.Date != membershipDues?[i -1].MemRenewalDate.Date && membershipDues[i].MembershipType.Name == membershipDues?[i - 1].MembershipType.Name)
                    {
                        consecutiveMember = membershipDues[i].MemStartDate;
                        break;
                    }
                    else if(membershipDues[i -1].MemCancelDate != null && membershipDues[i].MemStartDate.Date != membershipDues[i - 1].MemCancelDate && membershipDues[i].MembershipType.Name == membershipDues?[i - 1].MembershipType.Name)
                    {
                        consecutiveMember = membershipDues[i].MemStartDate;
                        break;
                    }
                }
                return consecutiveMember;
            }
            return null;
        }
    }
}
