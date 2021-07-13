using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Please enter employee's First Name")]
        [StringLength(60)]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please enter employee's Last Name")]
        [StringLength(60)]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please enter employee's Gender")]
        [StringLength(20)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please assign the employee's Position")]
        [StringLength(60)]
        public string Position { get; set; }
        [Required(ErrorMessage = "Please estimate employee's Salary")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage = "Please enter employee's Address")]
        [StringLength(255)]
        public string Addr1 { get; set; }
        [StringLength(50)]
        public string Addr2 { get; set; }
        [Required(ErrorMessage = "Please enter employee's City")]
        [StringLength(60)]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter employee's State")]
        [StringLength(60)]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter employee's Postal Code")]
        [DataType(DataType.PostalCode)]
        public int? PostalCode { get; set; }
        [Required(ErrorMessage = "Please enter employee's Country")]
        [StringLength(60)]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please enter employee's BirthDate")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Please enter employee's Username")]
        [StringLength(25)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter employee's Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        [Compare("EmailConfirmed", ErrorMessage = "Employee's email does not match")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please confirm employee's Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        public string EmailConfirmed { get; set; }
        [Required(ErrorMessage = "Please enter employee's Temporary Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        [Compare("TemporaryPasswordConfirmed", ErrorMessage = "Employee's password does not match")]
        public string TemporaryPassword { get; set; }
        [Required(ErrorMessage = "Please confirm employee's Temporary Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string TemporaryPasswordConfirmed { get; set; }


        public bool IsChangingLogininformation { get; set; }

        public string UserID { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
