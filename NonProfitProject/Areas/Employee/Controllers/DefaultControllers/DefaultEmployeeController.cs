using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NonProfitProject.Areas.Employee.Data;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Employee.Controllers.DefaultControllers
{
    public class DefaultEmployeeController : Controller
    {
        private NonProfitContext nonProfitContext;
        public DefaultEmployeeController(NonProfitContext nonProfitContext)
        {
            this.nonProfitContext = nonProfitContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            HttpContext.Session.SetString("CommitteeName",CommitteeStatus.GetName(nonProfitContext, User.FindFirstValue(ClaimTypes.NameIdentifier))); 
        }
    }
}
