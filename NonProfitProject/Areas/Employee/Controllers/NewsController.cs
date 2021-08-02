using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NonProfitProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using NonProfitProject.Areas.Employee.Controllers.DefaultControllers;
using NonProfitProject.Areas.Employee.Data;

namespace NonProfitProject.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee")]
    [Area("Employee")]
    public class NewsController : DefaultEmployeeController
    {
        private NonProfitContext context;
        public NewsController(NonProfitContext context) : base(context)
        {
            this.context = context;
        }
        public bool isNewsCommittee()
        {
            var name = CommitteeStatus.GetName(context, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (name == "News Committee")
            {
                return true;
            }
            return false;
        }

        public IActionResult Index()
        {
            if (!isNewsCommittee())
            {
                return RedirectToAction("Index", "Home");
            }
            var news = context.News.ToList();
            return View(news);
        }
        [HttpGet]
        public IActionResult AddNews()
        {
            if (!isNewsCommittee())
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Action = "Add";
            return View("EditNews", new News());
        }

        [HttpGet]
        public IActionResult EditNews(int id)
        {
            if (!isNewsCommittee())
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Action = "Edit";
            var news = context.News.Find(id);
            if (news == null)
            {
                return RedirectToAction("Index");
            }
            return View(news);
        }

        [HttpPost]
        public IActionResult EditNews(News model)
        {
            if (!isNewsCommittee())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                string addOrEdit;
                if (model.NewsID == 0 || context.News.AsNoTracking().Where(n => n.NewsID == model.NewsID).FirstOrDefault() == null)
                {
                    var user = context.Users.Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
                    model.NewsID = 0;
                    model.NewsPublishDate = DateTime.UtcNow;
                    model.NewsLastUpdated = DateTime.UtcNow;
                    model.CreatedBy = user.UserFirstName + " " + user.UserLastName;
                    context.News.Add(model);
                    addOrEdit = "added";
                }
                else
                {
                    var news = context.News.Where(n => n.NewsID == model.NewsID).FirstOrDefault();
                    news.NewsLastUpdated = DateTime.UtcNow;
                    news.NewsTitle = model.NewsTitle;
                    news.NewsHeader = model.NewsHeader;
                    news.NewsBody = model.NewsBody;
                    context.News.Update(news);
                    addOrEdit = "updated";
                }

                context.SaveChanges();
                TempData["EventChanges"] = String.Format("The Event \"{0}\" has been {1}.", model.NewsTitle, addOrEdit);
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.Action = (model.NewsID == 0) ? "Add" : "Edit";
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!isNewsCommittee())
            {
                return RedirectToAction("Index", "Home");
            }
            var news = context.News.Find(id);
            context.News.Remove(news);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
