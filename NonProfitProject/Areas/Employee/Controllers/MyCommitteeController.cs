using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Areas.Employee.Data;
using NonProfitProject.Models;
using NonProfitProject.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Employee.Controllers
{
    //If user is admin, shows this page.
    [Authorize(Roles = "Employee")]
    [Area("Employee")]
    public class MyCommitteeController : Controller
    {
        private readonly NonProfitContext context;
        private readonly UserManager<User> userManager;
        public MyCommitteeController(NonProfitContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            HttpContext.Session.SetString("CommitteePosition", CommitteeStatus.GetPosition(this.context, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public async Task<IActionResult> Index()
        {
            
            var user = await userManager.GetUserAsync(User);
            var committee = context.Committees.Include(c => c.committeeMembers).ThenInclude(cm => cm.employee).ThenInclude(e => e.User).Where(c => c.CommitteesID == context.CommitteeMembers.Include(cm => cm.employee).Where(cm => cm.employee.UserID == user.Id).FirstOrDefault().CommitteeID).AsNoTracking().FirstOrDefault();
            if (committee == null)
            {
                return RedirectToAction("Index");
            }
            //creates session model
            var CommitteeMemberModel = new CommitteeMemberViewModel() { Committee = context.Committees?.Where(c => c.CommitteeName == committee.CommitteeName).AsNoTracking().FirstOrDefault(), Employees = context.Employees.Include(e => e.User).Where(e => !context.CommitteeMembers.Any(cm => e.EmpID == cm.EmpID)).AsNoTracking().ToList() };
            HttpContext.Session.SetObject("CommitteeMemberModel", CommitteeMemberModel);
            return View(committee);
        }


        [Route("~/[area]/[controller]/{name}/[action]/{id}")]
        public IActionResult MemberDetails(string id)
        {
            var employee = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).Where(e => e.EmpID == id).FirstOrDefault();
            var sessionmodel = HttpContext.Session.GetObject<CommitteeMemberViewModel>("CommitteeMemberModel");
            if (employee == null || sessionmodel == null)
            {
                TempData["AddCommitteeMemberTimeout"] = "Session Timeout";
                return RedirectToAction("Index");
            }
            ViewBag.Action = "MemberDetails";
            //resets the session time
            HttpContext.Session.SetObject("CommitteeMemberModel", sessionmodel);
            return View("EmployeeDetails", employee);
        }

        [HttpPost]
        [Route("~/[area]/[controller]/[action]/{id}/{name}/{position}")]
        public IActionResult EditPosition(string id,string name, string position)
        {
            if (!HttpContext.Session.GetString("CommitteePosition").Equals("President") && !HttpContext.Session.GetString("CommitteePosition").Equals("Vice President"))
            {
                return RedirectToAction("Index");
            }
            var member = context.CommitteeMembers.Include(cm => cm.employee).ThenInclude(e => e.User).Where(cm => cm.EmpID == id).FirstOrDefault();
            if(member == null || position.Length == 0)
            {
                return RedirectToAction("Index");
            }
            member.CommitteePosition = position;
            context.CommitteeMembers.Update(member);
            context.SaveChanges();
            TempData["CommitteeMemberChanges"] = String.Format("{0}'s position has been updated", member.employee.User.UserFirstName + " " + member.employee.User.UserLastName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveMember(string id)
        {
            if (!HttpContext.Session.GetString("CommitteePosition").Equals("President") && !HttpContext.Session.GetString("CommitteePosition").Equals("Vice President"))
            {
                return RedirectToAction("Index");
            }
            var employee = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).Where(e => e.EmpID == id).AsNoTracking().FirstOrDefault();
            var committeeID = employee.CommitteeMembers.CommitteeID;
            if(employee == null)
            {
                return RedirectToAction("Index");
            }
            TempData["CommitteeMemberChanges"] = String.Format("{0} has been removed from the committee", employee.User.UserFirstName + " " + employee.User.UserLastName);
            context.CommitteeMembers.Remove(employee.CommitteeMembers);
            context.SaveChanges();
            
            return RedirectToAction("Index");
        }


        [Route("~/[area]/[controller]/{name}/[action]")]
        [HttpGet]
        public IActionResult AddMembers(string name)
        {
            if (!HttpContext.Session.GetString("CommitteePosition").Equals("President") && !HttpContext.Session.GetString("CommitteePosition").Equals("Vice President"))
            {
                return RedirectToAction("Index");
            }
            var CommitteeMemberModel = new CommitteeMemberViewModel() { Committee = context.Committees?.Where(c => c.CommitteeName == name).AsNoTracking().FirstOrDefault(), Employees = context.Employees.Include(e => e.User).Where(e => !context.CommitteeMembers.Any(cm => e.EmpID == cm.EmpID)).AsNoTracking().ToList() };
            HttpContext.Session.SetObject("CommitteeMemberModel", CommitteeMemberModel);
            return View();
        }


        [HttpPost]
        [Route("~/[area]/[controller]/{name}/[action]/{id}")]
        public IActionResult AddMembers(CommitteeMembers model, string id)
        {
            if (!HttpContext.Session.GetString("CommitteePosition").Equals("President") && !HttpContext.Session.GetString("CommitteePosition").Equals("Vice President"))
            {
                return RedirectToAction("Index");
            }
            var sessionmodel = HttpContext.Session.GetObject<CommitteeMemberViewModel>("CommitteeMemberModel");
            if (sessionmodel == null)
            {
                TempData["AddCommitteeMemberTimeout"] = "Session Timeout";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var committeeMember = new CommitteeMembers()
                {
                    CommitteeID = context.Committees.Where(c => c.CommitteeName == sessionmodel.Committee.CommitteeName).Select(c => c.CommitteesID).FirstOrDefault(),
                    EmpID = id,
                    CommitteePosition = model.CommitteePosition
                };
                context.CommitteeMembers.Add(committeeMember);
                context.SaveChanges();
                return RedirectToAction("AddMembers", new { name = sessionmodel.Committee.CommitteeName , id = ""});
            }
            return RedirectToAction("AddMembers", new { name = sessionmodel.Committee.CommitteeName, id = "" });
        }


        [Route("~/[area]/[controller]/{name}/AddMembers/[action]/{id}")]
        public IActionResult EmployeeDetails(string id)
        {
            if (!HttpContext.Session.GetString("CommitteePosition").Equals("President") && !HttpContext.Session.GetString("CommitteePosition").Equals("Vice President"))
            {
                return RedirectToAction("Index");
            }
            var employee = context.Employees.Include(e => e.User).Where(e => !context.CommitteeMembers.Any(cm => e.EmpID == cm.EmpID) && e.EmpID == id).FirstOrDefault();
            var sessionmodel = HttpContext.Session.GetObject<CommitteeMemberViewModel>("CommitteeMemberModel");
            if (employee == null || sessionmodel == null)
            {
                TempData["AddCommitteeMemberTimeout"] = "Session Timeout";
                return RedirectToAction("Index");
            }
            //resets the session time
            HttpContext.Session.SetObject("CommitteeMemberModel", sessionmodel);
            return View(employee);
        }
    }
}
