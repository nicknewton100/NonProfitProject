using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Code;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    //inherits DefaultMembershipController which is used for memberships in all areas
    public class MembershipController : NonProfitProject.Controllers.Shared.Users.DefaultMembershipController
    {    
        public MembershipController(NonProfitContext context, UserManager<User> userManager, IEmailManager emailManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.emailManager = emailManager;
        }
    }
}
