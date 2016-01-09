﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Enums;

namespace eUni.BusinessLogic.Providers.DataTransferObjects
{
    public class FileDTO
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public FileType FileType { get; set; }
        public int ModuleId { get; set; }
    }
}
