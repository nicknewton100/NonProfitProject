using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NonProfitProject.Models;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Areas.Users.Controllers;

namespace NonProfitProject.Areas.Employee.Controllers
{
    //If its not admin, dont allow use of the controller
    [Authorize(Roles = "Employee")]
    [Area("Employee")]
    public class ProfileController : DefaultProfileController
    {
        public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager, NonProfitContext context)
        {            
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }
    }
}
