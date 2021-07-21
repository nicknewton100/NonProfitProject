using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Models;
using NonProfitProject.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    //If user is admin, shows this page.
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]s/[action]/{id?}")]
    public class CommitteeController : Controller
    {
        private NonProfitContext context;
        public CommitteeController(NonProfitContext context)
        {
            this.context = context;
        }
        [Route("~/[area]/[controller]s")]
        public IActionResult Index()
        {
            var model = context.Committees.Include(c => c.committeeMembers).ThenInclude(cm => cm.employee).ThenInclude(e => e.User).ToList();
            return View(model);
        }
        [Route("~/[area]/[controller]/{name}")]
        public IActionResult Details(string name)
        {
            var committee = context.Committees.Include(c => c.committeeMembers).ThenInclude(cm => cm.employee).ThenInclude(e => e.User).Where(c => c.CommitteeName.Equals(name)).FirstOrDefault();
            return View(committee);
        }
        
        [Route("~/[area]/[controller]/{name}/[action]")]
        [HttpGet]
        public IActionResult AddMembers(string name)
        {
            var committee = context.Committees?.Where(c => c.CommitteeName == name).FirstOrDefault() ?? null;
            if(committee == null)
            {
                return RedirectToAction("Index");
            }
            var employees = context.Employees.Include(e => e.User).Where(e => !context.CommitteeMembers.Any(cm => e.EmpID == cm.EmpID)).ToList();
            var addCommitteeMemberModel = new AddCommitteeMemberViewModel() { Committee = committee, Employees = employees };
            HttpContext.Session.SetObject("AddCommitteeMemberModel",addCommitteeMemberModel);
            return View();
        }
        [HttpPost]
        [Route("~/[area]/[controller]/{name}/[action]/{id}")]
        public IActionResult AddMembers(CommitteeMembers model, string id)
        {
            var sessionmodel = HttpContext.Session.GetObject<AddCommitteeMemberViewModel>("AddCommitteeMemberModel");
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
        public IActionResult EmployeeDetails(string id)
        {
            return View();
        }
        public IActionResult AddCommittee()
        {
            ViewBag.Action = "Add";
            return View("EditCommittee");
        }       
        
        
        [HttpGet]
        public IActionResult EditCommittee(int id)
        {
            ViewBag.Action = "Edit";
            var Committee = context.Committees.Find(id);
            return View(Committee);
        }
        [HttpPost]
        public IActionResult EditCommittee(Committees model)
        {
            if (ModelState.IsValid)
            {
                string addOrEdit;
                if (model.CommitteesID == 0)
                {
                    context.Committees.Add(model);
                    addOrEdit = "added";
                }
                else
                {
                    context.Committees.Update(model);
                    addOrEdit = "updated";
                }

                context.SaveChanges();
                TempData["CommitteeChanges"] = String.Format("The \"{0}\" has been {1}.", model.CommitteeName, addOrEdit);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Action = (model.CommitteesID == 0) ? "Add" : "Edit";
                return View(model);
            }
        }
    }
}
