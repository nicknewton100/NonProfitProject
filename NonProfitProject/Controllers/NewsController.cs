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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewsPage()
        {
            var news = context.News.ToList();
            return View(news);
        }

    }
}
