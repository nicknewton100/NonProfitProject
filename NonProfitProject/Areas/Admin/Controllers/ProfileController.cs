using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NonProfitProject.Models;
using NonProfitProject.Controllers.Shared.Users;

namespace NonProfitProject.Areas.Admin.Controllers
{
    //If its not admin, dont allow use of the controller
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
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
