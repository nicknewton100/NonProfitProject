using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NonProfitProject.Models.ViewModels
{       //This model is used for login viewpage
    public class LoginViewModel
    {
        //holds login information data that has been posted and validation messages
        [Required(ErrorMessage = "Please enter a username. ")]
        [StringLength(60)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password. ")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}
