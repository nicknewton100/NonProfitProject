using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BackupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Backup()
        {
            SqlConnection sqlConnection = new SqlConnection("Server=tcp:cpt275.database.windows.net,1433;Initial Catalog=cpt275seniorproj;Persist Security Info=False;User ID=cpt275;Password=987963Gizm0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataAdapter sqlData = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var parent1 = Environment.CurrentDirectory.ToString();


            sqlConnection.Open();
            sqlCommand = new SqlCommand("backup database cpt275seniorproj to disk='" + parent1.ToString() + "\\Backup\\" + "test" + ".bak'", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            /*string filePath = "your file path";
            string fileName = ""your file name;

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);*/
            return RedirectToAction("Index");
        }
    }
}
