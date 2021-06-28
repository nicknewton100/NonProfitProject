using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NonProfitProject.Models
{
    public class User : IdentityUser
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserAddr1 { get; set; }
        public string UserAddr2 { get; set; }
        public string UserCity { get; set; }
        public string UserState { get; set; }
        public int UserPostalCode { get; set; }
        public string UserCountry { get; set; }
        public DateTime UserBirthDate { get; set; }
        public string UserGender { get; set; }
        public DateTime UserCreationDate { get; set; }
        public DateTime UserLastActivity { get; set; }
        public bool UserActive { get; set; }
        public bool ReceiveWeeklyNewsletter { get; set; }

        public ICollection<PaymentInformation> payments { get; set; }
        public ICollection<DonationReceipts> donationReceipts { get; set; }

        [NotMapped]
        public IList<string> RoleNames { get; set; }

    }
}
