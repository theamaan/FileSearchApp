using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FileSearchApp.Models
{
    public class FileModel
    {
        public string DirectoryPath { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
