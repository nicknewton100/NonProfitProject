using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Models.ViewModels
{
    public class MemberViewModel
    {
        //used for holding Member information and validation messages

        //user information
        public string id { get; set; }
        [Required(ErrorMessage = "Please enter member's First Name")]
        [StringLength(60)]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please enter member's Last Name")]
        [StringLength(60)]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please enter member's Gender")]
        [StringLength(20)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter member's Address")]
        [StringLength(255)]
        public string Addr1 { get; set; }
        [StringLength(50)]
        public string Addr2 { get; set; }
        [Required(ErrorMessage = "Please enter member's City")]
        [StringLength(60)]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter member's State")]
        [StringLength(60)]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter member's Postal Code")]
        [DataType(DataType.PostalCode)]
        public int? PostalCode { get; set; }
        [Required(ErrorMessage = "Please enter member's BirthDate")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter member's Username")]
        [StringLength(25)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter member's Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        [Compare("EmailConfirmed", ErrorMessage = "member's email does not match")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please confirm member's Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        public string EmailConfirmed { get; set; }
        [Required(ErrorMessage = "Please enter member's Temporary Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        [Compare("TemporaryPasswordConfirmed", ErrorMessage = "member's password does not match")]
        public string TemporaryPassword { get; set; }
        [Required(ErrorMessage = "Please confirm member's Temporary Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string TemporaryPasswordConfirmed { get; set; }

        //is used to check if the admin is making the user become a meber or not
        public bool IsBecomingMember { get; set; }

        //used to check if admin is editing user login
        public bool IsChangingLogininformation { get; set; }

        //membership information
        public string MembershipType { get; set; }
        [DataType(DataType.Date)]
        public DateTime? MemberSince { get; set; }

        //last membership
        [Required(ErrorMessage = "Please assign Start Date")]
        [DataType(DataType.Date)]
        public DateTime? LastRenewalDate { get; set; }
        [Required(ErrorMessage = "Please assign End Date")]
        [DataType(DataType.Date)]
        public DateTime? FutureEndDate { get; set; }
        [Required(ErrorMessage = "Please assign Renewal Date")]
        [DataType(DataType.Date)]
        public DateTime? FutureRenewalDate { get; set; }
    }
}
