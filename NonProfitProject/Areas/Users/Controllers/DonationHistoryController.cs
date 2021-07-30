using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Code.Security;
using System.Security.Claims;
using NonProfitProject.Controllers.Shared.Users;

namespace NonProfitProject.Areas.Users.Controllers
{
    [Authorize(Roles = "User")]
    [Area("Users")]
    public class DonationHistoryController : Controller
    {
        private NonProfitContext context;
        public DonationHistoryController(NonProfitContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var receipts = context.Receipts.Include(r => r.Donation).Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.User).Where(r => r.MembershipDue == null && r.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).OrderBy(r => r.Date).ToList();
            return View(receipts);
        }
        public IActionResult Details(int id)
        {
            var donation = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Include(r => r.User).Where(r => r.ReceiptID == id && r.MembershipDue == null && r.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
            if (donation == null)
            {
                return RedirectToAction("Index");
            }
            return View(donation);
        }
    }
}
