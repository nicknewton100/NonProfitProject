using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your First Name")]
        [StringLength(60)]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please enter your Last Name")]
        [StringLength(60)]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please enter your Address")]
        [StringLength(255)]
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        [Required(ErrorMessage = "Please enter your City")]
        [StringLength(60)]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your State")]
        [StringLength(60)]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter your Postal Code")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }
        [Required(ErrorMessage = "Please enter your Country")]
        [StringLength(60)]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please enter your BirthDate")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please enter your Gender")]
        [StringLength(20)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter your Username")]
        [StringLength(25)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        [Compare("EmailConfirmed")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please confirm your Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        public string EmailConfirmed { get; set; }
        [Required(ErrorMessage = "Please enter a Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        [Compare("PasswordConfirmed")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm your Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string PasswordConfirmed { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool recieveWeeklyNewsletter { get; set; }
        
    }
}
