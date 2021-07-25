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
            var receipts = context.Receipts.Include(r => r.Donation).Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.User).Where(r => r.MembershipDue == null).OrderBy(r => r.Date).ToList();
            return View(receipts);
        }
        public IActionResult Details(int id)
        {
            var donation = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Include(r => r.User).Where(r => r.ReceiptID == id).FirstOrDefault();
            if (donation == null)
            {
                return RedirectToAction("Index");
            }
            return View(donation);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var receipt = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Include(r => r.User).Where(r => r.ReceiptID == id).FirstOrDefault();
            if(receipt == null)
            {
                return RedirectToAction("Index");
            }
            var editDonation = new EditDonationViewModel
            {
                ReceiptID = receipt.ReceiptID,
                Username = receipt.User.UserName,
                DonationAmount = receipt.Donation.DonationAmount,
                FirstName = receipt.InvoiceDonorInformation.FirstName,
                LastName = receipt.InvoiceDonorInformation.LastName,
                Email = receipt.InvoiceDonorInformation.Email,
                Phone = receipt.InvoiceDonorInformation.Phone,
                Addr1 = receipt.InvoiceDonorInformation.Addr1,
                Addr2 = receipt.InvoiceDonorInformation.Addr2,
                City = receipt.InvoiceDonorInformation.City,
                State = receipt.InvoiceDonorInformation.State,
                PostalCode = receipt.InvoiceDonorInformation.PostalCode
            };
            return View(editDonation);
        }
        [HttpPost]
        public IActionResult Edit(EditDonationViewModel model)
        {
            var currentReceipt = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Include(r => r.User).Where(r => r.ReceiptID == model.ReceiptID).AsNoTracking().FirstOrDefault();
            model.Username = currentReceipt.User.UserName;
            if (!model.isChangingCardInformation)
            {
                ModelState.Remove("CardholderName");
                ModelState.Remove("CardType");
                ModelState.Remove("CardNumber");
                ModelState.Remove("ExpDate");
                ModelState.Remove("CVV");
                ModelState.Remove("BillingFirstName");
                ModelState.Remove("BillingLastName");
                ModelState.Remove("BillingAddr1");
                ModelState.Remove("BillingAddr2");
                ModelState.Remove("BillingCity");
                ModelState.Remove("BillingState");
                ModelState.Remove("BillingPostalCode");
            }
            if (ModelState.IsValid)
            {
                //sets receipt information
                currentReceipt.Total = model.DonationAmount;
                //sets donation information
                currentReceipt.Donation.DonationAmount = model.DonationAmount;
                //personal information
                currentReceipt.InvoiceDonorInformation.FirstName = model.FirstName;
                currentReceipt.InvoiceDonorInformation.LastName = model.LastName;
                currentReceipt.InvoiceDonorInformation.Email = model.Email;
                currentReceipt.InvoiceDonorInformation.Phone = model.Phone;
                currentReceipt.InvoiceDonorInformation.Addr1 = model.Addr1;
                currentReceipt.InvoiceDonorInformation.Addr2 = model.Addr2;
                currentReceipt.InvoiceDonorInformation.City = model.City;
                currentReceipt.InvoiceDonorInformation.State = model.State;
                currentReceipt.InvoiceDonorInformation.PostalCode = model.PostalCode;

                //billing information if it was checked to be changed
                if (model.isChangingCardInformation)
                {
                    AesEncryption aes = new AesEncryption();
                    currentReceipt.InvoicePayment.CardholderName = model.CardholderName;
                    currentReceipt.InvoicePayment.CardType = model.CardType;
                    currentReceipt.InvoicePayment.CardNumber = aes.Encrypt(model.CardNumber);
                    currentReceipt.InvoicePayment.ExpDate = model.ExpDate;
                    currentReceipt.InvoicePayment.CVV = model.CVV;
                    currentReceipt.InvoicePayment.Last4Digits = Int32.Parse(model.CardNumber.Substring(model.CardNumber.Length - 4));
                }
                context.Receipts.Update(currentReceipt);
                context.SaveChanges();
                TempData["DonationChanges"] = String.Format("The Donation with Receipt ID {0} has been updated", currentReceipt.ReceiptID);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var receipt = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Where(r => r.ReceiptID == id).FirstOrDefault();
            if(receipt == null)
            {
                TempData["DonationChanges"] = String.Format("The Donation with Receipt ID {0} does not exist", id);
                return RedirectToAction("Index");
            }
            context.Receipts.Remove(receipt);
            context.SaveChanges();
            TempData["DonationChanges"] = String.Format("The Donation with Receipt ID {0} has been deleted", id);
            return RedirectToAction("Index");
        }
    }
}
