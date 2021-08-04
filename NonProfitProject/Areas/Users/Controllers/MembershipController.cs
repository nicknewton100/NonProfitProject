using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Controllers.Shared.Users;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Users.Controllers
{
    [Authorize(Roles = "User,Member")]
    [Area("Users")]
    //inherits DefaultMembershipController
    public class MembershipController : DefaultMembershipController
    {
        public MembershipController(NonProfitContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
    }
}
