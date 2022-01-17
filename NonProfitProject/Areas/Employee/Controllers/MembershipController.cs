﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NonProfitProject.Areas.Employee.Data;
using NonProfitProject.Code;
using NonProfitProject.Controllers.Shared.Users;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee")]
    [Area("Employee")]
    //inherits DefaultMembershipController
    public class MembershipController : DefaultMembershipController
    {
        
        public MembershipController(NonProfitContext context, UserManager<User> userManager, IEmailManager emailManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.emailManager = emailManager;
        }
        //sets committee name on executing action
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            HttpContext.Session.SetString("CommitteeName", CommitteeStatus.GetName(this.context, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
