using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Models.ViewModels
{
    public class DonorViewModel
    {
        //used for holding donor information and validation messages
        public string id { get; set; }
        [Required(ErrorMessage = "Please enter donor's First Name")]
        [StringLength(60)]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please enter donor's Last Name")]
        [StringLength(60)]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please enter donor's Gender")]
        [StringLength(20)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter donor's Address")]
        [StringLength(255)]
        public string Addr1 { get; set; }
        [StringLength(50)]
        public string Addr2 { get; set; }
        [Required(ErrorMessage = "Please enter donor's City")]
        [StringLength(60)]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter donor's State")]
        [StringLength(60)]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter donor's Postal Code")]
        [DataType(DataType.PostalCode)]
        public int? PostalCode { get; set; }
        [Required(ErrorMessage = "Please enter donor's BirthDate")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Please enter donor's Username")]
        [StringLength(25)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter donor's Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        [Compare("EmailConfirmed", ErrorMessage = "Donor's email does not match")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please confirm donor's Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        public string EmailConfirmed { get; set; }
        [Required(ErrorMessage = "Please enter donor's Temporary Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        [Compare("TemporaryPasswordConfirmed", ErrorMessage = "Donor's password does not match")]
        public string TemporaryPassword { get; set; }
        [Required(ErrorMessage = "Please confirm donor's Temporary Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string TemporaryPasswordConfirmed { get; set; }

        //is used to check if the admin is making the user become a meber or not
        public bool IsBecomingMember { get; set; }

        //used to check if admin is editing user login
        public bool IsChangingLogininformation { get; set; }



    }
}
