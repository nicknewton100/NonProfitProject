using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Controllers.Shared.Users;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Member.Controllers
{
    //If its not admin, dont allow use of the controller
    [Authorize(Roles = "Member")]
    [Area("Member")]
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
