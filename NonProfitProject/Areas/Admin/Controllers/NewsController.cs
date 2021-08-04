using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NonProfitProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class NewsController : Controller
    {
        private NonProfitContext context;
        public NewsController(NonProfitContext context)
        {
            this.context = context;
        }
        //shows news based on newest first
        public IActionResult Index()
        {
            var news = context.News.OrderByDescending(n => n.NewsPublishDate).ToList();
            return View(news);
        }
        //goes to news page
        [HttpGet]
        public IActionResult AddNews()
        {
            ViewBag.Action = "Add";
            return View("EditNews", new News());
        }
        //gets the news object based on id and sends it to the webpage
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
        //gets the posted news object and gets edits the news object based on the id
        [HttpPost]
        public IActionResult EditNews(News model)
        {
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
                TempData["NewsChanges"] = String.Format("The News Article \"{0}\" has been {1}.", model.NewsTitle, addOrEdit);
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.Action = (model.NewsID == 0) ? "Add" : "Edit";
                return View(model);
            }
        }
        //deltes the news article based on id
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
