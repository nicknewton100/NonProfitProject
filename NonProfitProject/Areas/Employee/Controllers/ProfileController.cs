using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NonProfitProject.Models;
using NonProfitProject.Controllers.Shared.Users;
using NonProfitProject.Areas.Employee.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

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
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            HttpContext.Session.SetString("CommitteeName", CommitteeStatus.GetName(this.context, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
