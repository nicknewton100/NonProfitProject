using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.ViewModels.Shared.Users
{
    public class EditLoginViewModel
    {
        //validation for account editing
        public string userID { get; set; }
        [Required(ErrorMessage = "Please enter your Username")]
        [StringLength(25)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your Email Address")]
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(60)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a Password")]
        public string CurrentPassword { get; set; }
        
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string NewPasswordConfirmed { get; set; }
        public bool ReceiveWeeklyNewsletter { get; set; }
    }
}
