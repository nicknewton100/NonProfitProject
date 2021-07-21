using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DonationHistoryController : Controller
    {
        private NonProfitContext context;
        public DonationHistoryController(NonProfitContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var receipts = context.Receipts.Include(r => r.Donation).Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.User).OrderBy(r => r.Date).ToList();
            return View(receipts);
        }
        public IActionResult Details(int id)
        {
            TempData["Action"] = "Details";
            var donation = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Include(r => r.User).Where(r => r.ReceiptID == id).FirstOrDefault();
            return View(donation);
        }
    }
}
