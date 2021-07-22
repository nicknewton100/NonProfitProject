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


namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BackFilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BackupDatabase()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=cpt275.database.windows.net,1433;Initial Catalog=cpt275seniorproj;Persist Security Info=False;User ID=cpt275;Password=987963Gizm0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataAdapter sqlData = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var path = Environment.CurrentDirectory.ToString();
            string directory = @"..\\Backup";
            // check if backup folder exist, otherwise create it.
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string saveDirectory = directory + "\\" + DateTime.Now.ToString("dd_MM_yyyy_HHmmss") + ".bak";

            sqlConnection.Open();
            sqlCommand = new SqlCommand("backup database cpt275seniorproj to disk='" + saveDirectory +"'", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Backup()
        {
            var path = Environment.CurrentDirectory.ToString();
            string directory = @"..\\..\\..\\..\\Backup";
            // check if backup folder exist, otherwise create it.
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string saveDirectory = directory + "\\" + DateTime.Now.ToString("dd_MM_yyyy_HHmmss") + ".zip";
            ZipFile.CreateFromDirectory(path, saveDirectory);

            byte[] fileBytes = System.IO.File.ReadAllBytes(saveDirectory);

            return File(fileBytes, "application/force-download", "BackupApplication.zip");
            //return RedirectToAction("Index");
        }
    }
}
