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
using NonProfitProject.Areas.Employee.Data;
using Microsoft.AspNetCore.Http;
using NonProfitProject.Areas.Employee.Controllers.DefaultControllers;

namespace NonProfitProject.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee")]
    [Area("Employee")]
    public class DonationHistoryController : DefaultEmployeeController
    {
        private NonProfitContext context;
        public DonationHistoryController(NonProfitContext context) : base(context)
        {
            this.context = context;
        }
        //checks to see if the committee is the Fundraising Committee
        public bool isFundraisingCommitee()
        {
            var name = CommitteeStatus.GetName(context, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (name == "Fundraising Committee")
            {
                return true;
            } 
            return false;
        }
        //gets all the donations. If my donations is true or the user isnt apart of the fundraising committee it only shows their donations
        [HttpGet]
        public IActionResult Index(string MyDonations)
        {
            var receipts = context.Receipts.Include(r => r.Donation).Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.User).Where(r => r.MembershipDue == null).OrderByDescending(r => r.Date).ToList();
            if (!isFundraisingCommitee() || MyDonations == "true")
            {
                receipts = receipts.Where(r => r.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
                ViewBag.FinancialEmployee = isFundraisingCommitee();
                ViewBag.Donations = "You have not made any donations yet";
            }
            return View(receipts);
        }
        //displays details about the donations 
        public IActionResult Details(int id)
        {
            var donation = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Include(r => r.User).Where(r => r.ReceiptID == id && r.MembershipDue == null).ToList();

            if (!isFundraisingCommitee())
            {
                donation = donation.Where(d => d.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            }
            if (donation == null)
            {
                return RedirectToAction("Index");
            }
            return View(donation.FirstOrDefault());
        }
        //shows edit page if the user is apart of the fundraising committee
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!isFundraisingCommitee())
            {
                return RedirectToAction("Index");
            }
            var receipt = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Include(r => r.User).Where(r => r.ReceiptID == id && r.MembershipDue == null).FirstOrDefault();
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
        //allows edit if the employee is apart of the fundrasing committee and edits the changes
        [HttpPost]
        public IActionResult Edit(EditDonationViewModel model)
        {
            if (!isFundraisingCommitee())
            {
                return RedirectToAction("Index");
            }
            var currentReceipt = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Include(r => r.User).Where(r => r.ReceiptID == model.ReceiptID && r.MembershipDue == null).AsNoTracking().FirstOrDefault();
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

                    currentReceipt.InvoicePayment.BillingFirstName = model.BillingFirstName;
                    currentReceipt.InvoicePayment.BillingLastName = model.BillingLastName;
                    currentReceipt.InvoicePayment.BillingAddr1 = model.BillingAddr1;
                    currentReceipt.InvoicePayment.BillingAddr2 = model.BillingAddr2;
                    currentReceipt.InvoicePayment.BillingCity = model.BillingCity;
                    currentReceipt.InvoicePayment.BillingState = model.BillingState;
                    currentReceipt.InvoicePayment.BillingPostalCode = (int)model.BillingPostalCode;
                }
                context.Receipts.Update(currentReceipt);
                context.SaveChanges();
                TempData["DonationChanges"] = String.Format("The Donation with Receipt ID {0} has been updated", currentReceipt.ReceiptID);
                return RedirectToAction("Index");
            }
            return View();
        }

        //deletes the donation if the employee is apart of the fundraising committee
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!isFundraisingCommitee())
            {
                return RedirectToAction("Index", "Home");
            }
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
