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
    //default employee controller which inherited by all controllers in the employee area
    //this is done to calculate the committee name on execution to know what employee is in which committee
    public class DefaultEmployeeController : Controller
    {
        private NonProfitContext nonProfitContext;
        public DefaultEmployeeController(NonProfitContext nonProfitContext)
        {
            this.nonProfitContext = nonProfitContext;
        }
        //runs this on every action execute
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //holds committee name in session
            HttpContext.Session.SetString("CommitteeName",CommitteeStatus.GetName(nonProfitContext, User.FindFirstValue(ClaimTypes.NameIdentifier))); 
        }
    }
}
