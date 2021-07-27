using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.AspNetCore.Hosting;
using AspNetCore.Reporting;
using NonProfitProject.Models;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Identity;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BackupController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;
        private NonProfitContext context;
        private UserManager<NonProfitProject.Models.User> userManager;
        public BackupController(IWebHostEnvironment webHostEnvironment, NonProfitContext context, UserManager<NonProfitProject.Models.User> userManager)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            this.userManager = userManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        //credit https://weblogs.asp.net/shahar/generating-sql-backup-script-for-tables-amp-data-from-any-net-application-using-smo
        [HttpPost]
        public IActionResult BackupDatabase()
        {
            try
            {
                string directory = @"..\\BackupFiles\\Database";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string saveDirectory = Path.GetFullPath(directory).ToString() + "\\" + DateTime.Now.ToString("dd_MM_yyyy_HHmmss") + ".sql";

                StringBuilder sb = new StringBuilder();
                Server srv = new Server(new Microsoft.SqlServer.Management.Common.ServerConnection("tcp:cpt275.database.windows.net,1433", "cpt275", "987963Gizm0"));
                Database dbs = srv.Databases["cpt275seniorproj"];
                ScriptingOptions options = new ScriptingOptions();
                options.ScriptData = true;
                options.ScriptDrops = false;
                options.FileName = saveDirectory;
                options.EnforceScriptingOptions = true;
                options.ScriptSchema = true;
                options.IncludeHeaders = true;
                options.AppendToFile = true;
                options.Indexes = true;
                options.WithDependencies = true;
                List<string> tables = new List<string> { "__EFMigrationsHistory", "AspNetRoleClaims", "AspNetRoles", "AspNetUserClaims", "AspNetUserLogins", "AspNetUserRoles", "AspNetUsers", "AspNetUserTokens", "CommitteeMembers", "Committees", "Donations", "Employees", "Events", "InvoiceDonorInformation", "InvoicePayments", "MembershipDues", "News", "Receipts", "SavedPayments", "MembershipType" };
                foreach (var tbl in tables)
                {
                    dbs.Tables[tbl].EnumScript(options);
                }
                byte[] fileBytes = System.IO.File.ReadAllBytes(saveDirectory);
                return File(fileBytes, "application/force-download", "BackupApplication.sql");
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("Index");
            }           
        }

        [HttpPost]
        public IActionResult Backup()
        {
            string path = Environment.CurrentDirectory.ToString(); ;

            string directory = @"..\\BackupFiles\\Application";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string saveDirectory = directory + "\\" + DateTime.Now.ToString("dd_MM_yyyy_HHmmss") + ".zip";
            ZipFile.CreateFromDirectory(path, saveDirectory);

            byte[] fileBytes = System.IO.File.ReadAllBytes(saveDirectory);
            return File(fileBytes, "application/force-download", "BackupApplication.zip");
        }

        [HttpPost]
        public IActionResult EmployeeReport()
        {
            var employee = context.Employees.Include(e => e.User).OrderBy(e => e.User.UserLastName + ", " + e.User.UserFirstName).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Number");
            dt.Columns.Add("EmpPosition");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("EmpEmail");
            dt.Columns.Add("EmpPhone");
            dt.Columns.Add("EmpAddress");
            dt.Columns.Add("EmpHireDate");
            dt.Columns.Add("EmpReleaseDate");
            DataRow row;
            int number = 0;
            foreach (var i in employee)
            {
                number += 1;
                row = dt.NewRow();
                row["Number"] = number;
                row["EmpPosition"] = i.Position;
                row["EmpName"] = i.User.UserLastName + ", " + i.User.UserFirstName;
                row["EmpEmail"] = i.User.Email;
                row["EmpPhone"] = i.User?.PhoneNumber ?? "Null";
                row["EmpAddress"] = String.Format("{0}, {1}{2}, {3} {4}", i.User.UserAddr1.Replace(".", "").TrimEnd(), i.User.UserAddr2 == "" ? i.User.UserAddr2 + ", " : "", i.User.UserCity, i.User.UserState, i.User.UserPostalCode);
                row["EmpHireDate"] = i.HireDate.ToShortDateString();
                row["EmpReleaseDate"] = i.ReleaseDate.HasValue ? i.ReleaseDate.Value.ToShortDateString() : "Null";
                dt.Rows.Add(row);
            }
            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\Reports\\rptEmployeeContact.rdlc";
            
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsEmployee", dt);
            //localReport.AddDataSource("dsUsers",context)
            var result = localReport.Execute(RenderType.Pdf,extension ,parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }

        [HttpPost]
        public async Task<IActionResult> DonorReport()
        {
            var users = await userManager.GetUsersInRoleAsync("User");
            var donor = context.Users.Where(u => users.Contains(u)).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Number");
            dt.Columns.Add("DonorName");
            dt.Columns.Add("DonorEmail");
            dt.Columns.Add("DonorPhone");
            dt.Columns.Add("DonorAddress");
            dt.Columns.Add("DonorJoinDate");
            DataRow row;
            int number = 0;
            foreach (var i in donor)
            {
                number += 1;
                row = dt.NewRow();
                row["Number"] = number;
                row["DonorName"] = i.UserLastName + ", " + i.UserFirstName;
                row["DonorEmail"] = i.Email;
                row["DonorPhone"] = i.PhoneNumber == null ? "Null": i.PhoneNumber;
                row["DonorAddress"] = String.Format("{0}, {1}{2}, {3} {4}", i.UserAddr1.Replace(".", "").TrimEnd(), i.UserAddr2 == "" ? i.UserAddr2 + ", " : "", i.UserCity, i.UserState, i.UserPostalCode);
                row["DonorJoinDate"] = i.UserCreationDate.ToShortDateString();
                dt.Rows.Add(row);
            }
            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\Reports\\rptDonorContact.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsDonorInformation", dt);
            //localReport.AddDataSource("dsUsers",context)
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }

        [HttpPost]
        public async Task<IActionResult> MemberReport()
        {
            var users = await userManager.GetUsersInRoleAsync("Member");
            var members = context.Users.Where(u => users.Contains(u) && context.MembershipDues.Any(md => md.UserID == u.Id)).Include(u => u.MembershipDues).ThenInclude(md => md.MembershipType).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).AsNoTracking().ToList();

            var membersNull = context.Users.Where(u => users.Contains(u) && !context.MembershipDues.Any(md => md.UserID == u.Id)).Include(u => u.MembershipDues).ThenInclude(md => md.MembershipType).OrderBy(u => u.UserLastName + ", " + u.UserFirstName).AsNoTracking().ToList();
            members.AddRange(membersNull);
            DataTable dt = new DataTable();
            dt.Columns.Add("Number");
            dt.Columns.Add("MemName");
            dt.Columns.Add("MemType");
            dt.Columns.Add("MemJoinDate");
            dt.Columns.Add("MemEmail");
            dt.Columns.Add("MemPhone");
            dt.Columns.Add("MemAddress");
            
            DataRow row;
            int number = 0;
            foreach (var i in members)
            {
                number += 1;
                row = dt.NewRow();
                row["Number"] = number;
                row["MemName"] = i.UserLastName + ", " + i.UserFirstName;
                row["MemType"] = i.MembershipDues.Count() == 0 ? "Null" : i.MembershipDues.Last().MembershipType.Name;
                row["MemJoinDate"] = MembershipDues.GetConsecutiveDate(i.MembershipDues.ToList())?.ToShortDateString() ?? "Null";
                row["MemEmail"] = i.Email;
                row["MemPhone"] = i.PhoneNumber == null ? "Null" : i.PhoneNumber;
                row["MemAddress"] = String.Format("{0}, {1}{2}, {3} {4}", i.UserAddr1.Replace(".", "").TrimEnd(), i.UserAddr2 == "" ? i.UserAddr2 + ", " : "", i.UserCity, i.UserState, i.UserPostalCode);
                
                dt.Rows.Add(row);
            }
            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\Reports\\rptMemberContact.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsMemberInformation", dt);
            //localReport.AddDataSource("dsUsers",context)
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }


        [HttpPost]
        public IActionResult CommitteeMemberReport()
        {
            var CommMem = context.CommitteeMembers.Include(cm => cm.committee).Include(cm => cm.employee).ThenInclude(e => e.User).OrderBy(cm => cm.committee.CommitteeName + " " + cm.employee.User.UserLastName + ", " + cm.employee.User.UserFirstName).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Number");
            dt.Columns.Add("CommName");
            dt.Columns.Add("CommPosition");
            dt.Columns.Add("CommMemName");
            dt.Columns.Add("CommMemEmail");
            dt.Columns.Add("CommMemPhone");
            dt.Columns.Add("CommMemAddress");
            DataRow row;
            int number = 0;
            foreach (var i in CommMem)
            {
                number += 1;
                row = dt.NewRow();
                row["Number"] = number;
                row["CommName"] = i.committee.CommitteeName.Replace(" Committee", "");
                row["CommPosition"] = i.CommitteePosition;
                row["CommMemName"] = i.employee.User.UserLastName + ", " + i.employee.User.UserFirstName;
                row["CommMemEmail"] = i.employee.User.Email;
                row["CommMemPhone"] = i.employee.User.PhoneNumber == null ? "Null" : i.employee.User.PhoneNumber;
                row["CommMemAddress"] = String.Format("{0}, {1}{2}, {3} {4}", i.employee.User.UserAddr1.Replace(".", "").TrimEnd(), i.employee.User.UserAddr2 == "" ? i.employee.User.UserAddr2 + ", " : "", i.employee.User.UserCity, i.employee.User.UserState, i.employee.User.UserPostalCode);
                dt.Rows.Add(row);
            }
            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\Reports\\rptCommitteeMemberContact.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsCommitteeMemberInformation", dt);
            //localReport.AddDataSource("dsUsers",context)
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }

        [HttpPost]
        public IActionResult DonationReport()
        {
            var donations = context.Receipts.Include(r => r.User).Include(r => r.InvoiceDonorInformation).Include(r => r.InvoicePayment).Include(r => r.Donation).Where(r => !context.MembershipDues.Any(md => md.ReceiptID == r.ReceiptID)).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ReceiptID");
            dt.Columns.Add("DonationName");
            dt.Columns.Add("DonationEmail");
            dt.Columns.Add("DonationAmount");
            dt.Columns.Add("DonationCardNumber");
            dt.Columns.Add("DonationDate");

            DataRow row;
            foreach (var i in donations)
            {
                row = dt.NewRow();
                row["ReceiptID"] = i.ReceiptID;
                row["DonationName"] = i.InvoiceDonorInformation.LastName + ", " + i.InvoiceDonorInformation.FirstName;
                row["DonationEmail"] = i.InvoiceDonorInformation.Email;
                row["DonationAmount"] = i.Total;
                row["DonationCardNumber"] = "xxxx-xxxx-xxxx-" + i.InvoicePayment.Last4Digits.ToString();
                row["DonationDate"] = i.Date.ToShortDateString();
                dt.Rows.Add(row);
            }

            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\Reports\\rptDonation.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsDonation", dt);
            //localReport.AddDataSource("dsUsers",context)
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }


        [HttpPost]
        public IActionResult MembershipDuesReport()
        {
            var donations = context.Receipts.Include(r => r.User).Include(r => r.InvoicePayment).Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Where(r => !context.Donations.Any(md => md.ReceiptID == r.ReceiptID)).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ReceiptID");
            dt.Columns.Add("MemName");
            dt.Columns.Add("MemEmail");
            dt.Columns.Add("Memtype");
            dt.Columns.Add("MemAmount");
            dt.Columns.Add("MemStartDate");
            dt.Columns.Add("MemEndDate");
            dt.Columns.Add("MemBillingDate");
            dt.Columns.Add("MemStatus");
            dt.Columns.Add("MemCancelDate");
            dt.Columns.Add("MemCardNumber");

            DataRow row;
            foreach (var i in donations)
            {
                row = dt.NewRow();
                row["ReceiptID"] = i.ReceiptID;
                row["MemName"] = i.User.UserLastName + ", " + i.User.UserFirstName;
                row["MemEmail"] = i.User.Email;
                row["MemType"] = i.MembershipDue.MembershipType.Name;
                row["MemAmount"] = i.Total;
                row["MemStartDate"] = i.MembershipDue.MemStartDate.ToShortDateString();
                row["MemEndDate"] = i.MembershipDue.MemEndDate.ToShortDateString();
                row["MemBillingDate"] = i.MembershipDue.MemRenewalDate.ToShortDateString();
                if (i.MembershipDue.MemActive)
                {
                    row["MemStatus"] = "Active";
                    row["MemCancelDate"] = "Null";
                }
                else
                {
                    if(i.MembershipDue.MemCancelDate != null)
                    {
                        row["MemStatus"] = "Canceled";
                        row["MemCancelDate"] = i.MembershipDue.MemCancelDate.Value.ToShortDateString();
                    }
                    else
                    {
                        row["MemStatus"] = "Expired";
                        row["MemCancelDate"] = "Null";
                    }
                }
                row["MemCardNumber"] = "xxxx-xxxx-xxxx-" + i.InvoicePayment.Last4Digits.ToString();
                dt.Rows.Add(row);
            }

            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\Reports\\rptMembershipDues.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsMemberDues", dt);
            //localReport.AddDataSource("dsUsers",context)
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }
    }
}
