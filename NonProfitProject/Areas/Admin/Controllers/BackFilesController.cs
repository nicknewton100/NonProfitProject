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
            try
            {
                string directory = @"..\\Backup";
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
                dbs.Script(options);
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
            }
            /*byte[] fileBytes = System.IO.File.ReadAllBytes(saveDirectory);
            return File(fileBytes, "application/force-download", "BackupApplication.sql");*/

            return RedirectToAction("Index");
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
    }
}
