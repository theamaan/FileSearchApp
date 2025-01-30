using FileSearchApp.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace FileSearchApp.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(FileModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if directory exists
                    if (!Directory.Exists(model.DirectoryPath))
                    {
                        ViewBag.Message = "Directory does not exist.";
                        return View("Index");
                    }

                    // Search for the file in the directory
                    string[] files = Directory.GetFiles(model.DirectoryPath);
                    string foundFile = files.FirstOrDefault(f => Path.GetFileName(f).Equals(model.FileName, StringComparison.OrdinalIgnoreCase));

                    if (foundFile != null)
                    {
                        model.FilePath = foundFile;
                        ViewBag.Message = "File Found!";
                        return View("Index", model);
                    }
                    else
                    {
                        ViewBag.Message = "File not found.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error: " + ex.Message;
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Download(FileModel model)
        {
            if (System.IO.File.Exists(model.FilePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(model.FilePath);
                return File(fileBytes, "application/octet-stream", Path.GetFileName(model.FilePath));
            }
            else
            {
                ViewBag.Message = "File does not exist.";
                return View("Index"); 
            }
        }
    }
}
