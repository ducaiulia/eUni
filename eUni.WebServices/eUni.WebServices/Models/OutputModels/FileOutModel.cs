using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Enums;

namespace eUni.WebServices.Models.OutputModels
{
    public class FileOutModel
    {
        //returns only files for module, not for student !!
        public int Id { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public FileType FileType { get; set; }
        public NamedEntityOutModel Module { get; set; }
    }
}