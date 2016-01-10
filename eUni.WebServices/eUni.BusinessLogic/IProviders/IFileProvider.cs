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
        int SaveUploadedFilePath(FileDTO fileDTO, int moduleId = -1, int hwId = -1);
        void DeleteFileWithId(int fileId);
        List<FileDTO> GetByModule(int moduleId);
    }

}
