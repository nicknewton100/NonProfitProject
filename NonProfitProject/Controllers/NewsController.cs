using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models;

namespace NonProfitProject.Controllers
{
    public class NewsController : Controller
    {
        private NonProfitContext context;
        public NewsController(NonProfitContext context)
        {
            this.context = context;
        }
        //displays all news based on when it was published. Newest first
        public IActionResult Index()
        {
            var news = context.News.OrderByDescending(n => n.NewsPublishDate).ToList();
            return View(news);
        }
    }
}
