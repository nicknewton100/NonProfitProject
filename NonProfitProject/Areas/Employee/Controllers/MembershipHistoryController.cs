using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Areas.Employee.Controllers.DefaultControllers;
using NonProfitProject.Areas.Employee.Data;
using NonProfitProject.Code.Security;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee")]
    [Area("Employee")]
    public class MembershipHistoryController : DefaultEmployeeController
    {
        private NonProfitContext context;
        private UserManager<User> userManager;

        public MembershipHistoryController(NonProfitContext context, UserManager<User> userManager): base(context)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public bool isFundrasingCommitee()
        {
            var name = CommitteeStatus.GetName(context, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (name == "Fundrasing Committee")
            {
                return true;
            }
            return false;
        }
        public IActionResult Index()
        {
            if (!isFundrasingCommitee())
            {
                return RedirectToAction("Index", "Home");
            }
            var receipts = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null).OrderByDescending(r => r.Date).ToList();
            return View(receipts);
        }

        public IActionResult Details(int id)
        {
            if (!isFundrasingCommitee())
            {
                return RedirectToAction("Index", "Home");
            }
            var membership = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null && r.ReceiptID == id).OrderBy(r => r.Date).FirstOrDefault();
            if (membership == null)
            {
                return RedirectToAction("Index");
            }
            EditMembershipDueViewModel model = new EditMembershipDueViewModel()
            {
                ReceiptID = membership.ReceiptID,
                Username = membership.User.UserName,
                Total = membership.MembershipDue.MembershipType.Amount,
                FirstName = membership.User.UserFirstName,
                LastName = membership.User.UserLastName,
                Email = membership.User.Email,
                Phone = membership.User.PhoneNumber,
                MembershipType = membership.MembershipDue.MembershipType.Name,
                StartDate = membership.MembershipDue.MemStartDate,
                EndDate = membership.MembershipDue.MemEndDate,
                RenewalDate = membership.MembershipDue.MemRenewalDate,
                CancelDate = membership.MembershipDue.MemCancelDate,
                Active = membership.MembershipDue.MemActive,
                CardholderName = membership.InvoicePayment.CardholderName,
                CardNumber = membership.InvoicePayment.Last4Digits.ToString(),
                CardType = membership.InvoicePayment.CardType,
                ExpDate = membership.InvoicePayment.ExpDate,
                BillingFirstName = membership.InvoicePayment.BillingFirstName,
                BillingLastName = membership.InvoicePayment.BillingLastName,
                BillingAddr1 = membership.InvoicePayment.BillingAddr1,
                BillingAddr2 = membership.InvoicePayment.BillingAddr2,
                BillingCity = membership.InvoicePayment.BillingCity,
                BillingState = membership.InvoicePayment.BillingState,
                BillingPostalCode = membership.InvoicePayment.BillingPostalCode

            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!isFundrasingCommitee())
            {
                return RedirectToAction("Index", "Home");
            }
            var membership = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null && r.ReceiptID == id).OrderBy(r => r.Date).FirstOrDefault();
            if (membership == null)
            {
                return RedirectToAction("Index");
            }
            var editMembership = new EditMembershipDueViewModel
            {
                ReceiptID = membership.ReceiptID,
                Username = membership.User.UserName,
                Total = membership.MembershipDue.MembershipType.Amount,
                FirstName = membership.User.UserFirstName,
                LastName = membership.User.UserLastName,
                Email = membership.User.Email,
                Phone = membership.User.PhoneNumber,
                MembershipType = membership.MembershipDue.MembershipType.Name,
                StartDate = membership.MembershipDue.MemStartDate,
                EndDate = membership.MembershipDue.MemEndDate,
                RenewalDate = membership.MembershipDue.MemRenewalDate,
            };
            return View(editMembership);
        }

        [HttpPost]
        public IActionResult Edit(EditMembershipDueViewModel model)
        {
            if (!isFundrasingCommitee())
            {
                return RedirectToAction("Index", "Home");
            }
            var currentMembership = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null && r.ReceiptID == model.ReceiptID).OrderBy(r => r.Date).FirstOrDefault();
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
                currentMembership.Total = model.Total;
                //sets membership due
                currentMembership.MembershipDue.MembershipTypeID = context.MembershipTypes.Where(mt => mt.Name == model.MembershipType).FirstOrDefault().MembershipTypeID;
                currentMembership.MembershipDue.MemStartDate = model.StartDate;
                currentMembership.MembershipDue.MemEndDate = model.EndDate;
                currentMembership.MembershipDue.MemRenewalDate = model.RenewalDate;

                //billing information if it was checked to be changed
                if (model.isChangingCardInformation)
                {
                    AesEncryption aes = new AesEncryption();
                    currentMembership.InvoicePayment.CardholderName = model.CardholderName;
                    currentMembership.InvoicePayment.CardType = model.CardType;
                    currentMembership.InvoicePayment.CardNumber = aes.Encrypt(model.CardNumber);
                    currentMembership.InvoicePayment.ExpDate = model.ExpDate;
                    currentMembership.InvoicePayment.CVV = model.CVV;
                    currentMembership.InvoicePayment.Last4Digits = Int32.Parse(model.CardNumber.Substring(model.CardNumber.Length - 4));

                    currentMembership.InvoicePayment.BillingFirstName = model.BillingFirstName;
                    currentMembership.InvoicePayment.BillingLastName = model.BillingLastName;
                    currentMembership.InvoicePayment.BillingAddr1 = model.BillingAddr1;
                    currentMembership.InvoicePayment.BillingAddr2 = model.BillingAddr2;
                    currentMembership.InvoicePayment.BillingCity = model.BillingCity;
                    currentMembership.InvoicePayment.BillingState = model.BillingState;
                    currentMembership.InvoicePayment.BillingPostalCode = (int)model.BillingPostalCode;
                }
                context.Receipts.Update(currentMembership);
                context.SaveChanges();
                TempData["MembershipDueChanges"] = String.Format("The Membership Due with Receipt ID {0} has been updated", currentMembership.ReceiptID);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!isFundrasingCommitee())
            {
                return RedirectToAction("Index", "Home");
            }
            var receipt = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.MembershipDue).Where(r => r.ReceiptID == id && r.Donation == null).FirstOrDefault();
            if (receipt == null)
            {
                TempData["MembershipDueChanges"] = String.Format("The Membership Due with Receipt ID {0} does not exist", id);
                return RedirectToAction("Index");
            }
            context.Receipts.Remove(receipt);
            context.SaveChanges();
            TempData["MembershipDueChanges"] = String.Format("The Membership Due with Receipt ID {0} has been deleted", id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelMembership(int id)
        {
            if (!isFundrasingCommitee())
            {
                return RedirectToAction("Index", "Home");
            }
            var receipt = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.MembershipDue).Where(r => r.ReceiptID == id && r.Donation == null && r.MembershipDue.MemActive).FirstOrDefault();
            if (receipt == null)
            {
                TempData["MembershipDueChanges"] = String.Format("The Membership Due with Receipt ID {0} does not exist", id);
                return RedirectToAction("Index");
            }
            receipt.MembershipDue.MemActive = false;
            receipt.MembershipDue.MemCancelDate = DateTime.UtcNow;
            var user = context.Users.Where(u => u.Id == receipt.MembershipDue.UserID).FirstOrDefault();
            if(user != null)
            {
                var result = await userManager.RemoveFromRoleAsync(user, "Member");
                if (!await userManager.IsInRoleAsync(user, "Admin") && !await userManager.IsInRoleAsync(user, "Employee"))
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
            context.Receipts.Update(receipt);
            context.SaveChanges();
            TempData["MembershipDueChanges"] = String.Format("The Membership Due with Receipt ID {0} has been canceled", id);
            return RedirectToAction("Index");
        }
    }
}
