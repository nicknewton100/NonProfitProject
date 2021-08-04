using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NonProfitProject.Models;

namespace NonProfitProject.Models
{
    //inherits identity userr which already has some basic variables.
    public class User : IdentityUser
    {
        //holds all user information including password hash, username, email, firstname, lastname etc.
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserAddr1 { get; set; }
        public string UserAddr2 { get; set; }
        public string UserCity { get; set; }
        public string UserState { get; set; }
        public int UserPostalCode { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime UserBirthDate { get; set; }
        public string UserGender { get; set; }
        public DateTime UserCreationDate { get; set; }
        public DateTime UserLastActivity { get; set; }
        public bool UserActive { get; set; }
        public bool ReceiveWeeklyNewsletter { get; set; }
        public bool AccountDisabled { get;set; }

        public ICollection<SavedPaymentInformation> SavedPayments { get; set; }
        public ICollection<MembershipDues> MembershipDues { get; set; }
        public ICollection<Donations> Donations { get; set; }
        public ICollection<Receipts> Receipts { get; set; }

        [NotMapped]
        public IList<string> RoleNames { get; set; }

    }
}
