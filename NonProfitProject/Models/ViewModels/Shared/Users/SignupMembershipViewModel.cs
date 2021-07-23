using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.ViewModels.Shared.Users
{
    public class SignupMembershipViewModel
    {
        public DonationPaymentViewModel PaymentViewModel { get; set; }
        public MembershipDues Membership { get; set; }
    }
}
