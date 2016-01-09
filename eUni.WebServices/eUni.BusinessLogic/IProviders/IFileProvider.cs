using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Enums;

namespace eUni.BusinessLogic.IProviders
{
    public interface IFileProvider
    {
        bool SaveUploadedFilePath(FileDTO fileDTO);
    }
}
