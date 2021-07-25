using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MembershipController : NonProfitProject.Controllers.Shared.Users.DefaultMembershipController
    {    
        public MembershipController(NonProfitContext context, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }
    }
}
