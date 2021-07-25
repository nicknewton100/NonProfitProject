using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models
{
    public class MembershipType
    {
        public string MembershipTypeID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public ICollection<MembershipDues> membershipDues;
    }
}
