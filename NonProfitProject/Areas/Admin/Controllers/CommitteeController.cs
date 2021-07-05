using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NonProfitProject.Models;
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
        public IActionResult EditEvent(Committees model)
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
                TempData["EventChanges"] = String.Format("The \"{0}\" has been {1}.", model.CommitteeName, addOrEdit);
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
