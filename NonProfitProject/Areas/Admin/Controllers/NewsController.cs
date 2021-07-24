using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NonProfitProject.Models;
using Microsoft.EntityFrameworkCore;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class NewsController : Controller
    {
        private NonProfitContext context;
        public NewsController(NonProfitContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var news = context.News.ToList();
            return View(news);
        }
        [HttpGet]
        public IActionResult AddNews()
        {
            ViewBag.Action = "Add";
            return View("EditNews", new News());
        }

        [HttpGet]
        public IActionResult EditNews(int id)
        {
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
            if (ModelState.IsValid)
            {
                string addOrEdit;
                if (model.NewsID == 0 || context.News.AsNoTracking().Where(n => n.NewsID == model.NewsID).FirstOrDefault() == null)
                {
                    model.NewsID = 0;
                    context.News.Add(model);
                    addOrEdit = "added";
                }
                else
                {
                    context.News.Update(model);
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
            var news = context.News.Find(id);
            context.News.Remove(news);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
