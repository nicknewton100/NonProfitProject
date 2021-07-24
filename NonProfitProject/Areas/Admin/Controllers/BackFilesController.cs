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

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BackFilesController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;
        private NonProfitContext context;
        public BackFilesController(IWebHostEnvironment webHostEnvironment, NonProfitContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
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
                string directory = @"..\\Backup";
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
                List<string> tables = new List<string> { "__EFMigrationsHistory", "AspNetRoleClaims", "AspNetRoles", "AspNetUserClaims", "AspNetUserLogins", "AspNetUserRoles", "AspNetUsers", "AspNetUserTokens", "CommitteeMembers", "Committees", "Donations", "Employees", "Events", "InvoiceDonorInformation", "InvoicePayments", "MembershipDues", "News", "Receipts", "SavedPayments" };
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

            string directory = @"..\\Backup";

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
        public IActionResult Print()
        {
            var employee = context.Employees.ToList();
            DataTable dt = new DataTable();
            dt = getData(employee);

            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\Reports\\rptEmployee.rdlc";
            
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsEmployee", dt);
            //localReport.AddDataSource("dsUsers",context)
            var result = localReport.Execute(RenderType.Pdf,extension ,parameters, mimetype);

            return File(result.MainStream, "application/pdf");
        }

        public DataTable getData(List<Employees> context)
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpID");
            dt.Columns.Add("UserID");
            dt.Columns.Add("Position");
            DataRow row;
            foreach(var i in context)
            {
                row = dt.NewRow();
                row["EmpID"] = i.EmpID;
                row["UserID"] = i.UserID;
                row["Position"] = i.Position;
                dt.Rows.Add(row);
            }
            return dt;
        }
        /*public DataTable getUser(List<NonProfitProject.Models.User> context)
        {

        }*/
    }
}
