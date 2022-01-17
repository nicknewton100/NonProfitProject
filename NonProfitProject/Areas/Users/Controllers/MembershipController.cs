using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Code;
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
        public MembershipController(NonProfitContext context, UserManager<User> userManager, IEmailManager emailManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.emailManager = emailManager;
        }
    }
}
