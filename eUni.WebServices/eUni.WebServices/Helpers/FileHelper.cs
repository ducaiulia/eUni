using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Dropbox.Api;
using Dropbox.Api.Files;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Enums;

namespace eUni.WebServices.Helpers
{
    public static class FileHelper
    {
        public static async Task UploadFile(string username, string path, byte[] contentFile, IFileProvider fileProvider,
            string filename, FileType fileType, int moduleId = -1, int hwId = -1)
        {
            var key = WebConfigurationManager.AppSettings["DropboxToken"];
            var dbx = new DropboxClient(key);
            using (var memoryStream = new MemoryStream(contentFile))
            {
                FileMetadata uploaded;
                try
                {
                    uploaded = await dbx.Files.UploadAsync(
                        path,
                        WriteMode.Add.Instance,
                        body: memoryStream);
                    Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(username, "Upload File"));
                }
                catch (Exception ex)
                {
                    Logger.Logger.Instance.LogError(ex);
                    throw;
                }
                var input = new FileDTO
                {
                    Path = path,
                    FileType = fileType,
                    FileName= filename
                };
                if (fileProvider.SaveUploadedFilePath(input, moduleId, hwId) < 0)
                    throw new Exception("Cannot save file path");
            }
        }
    }
}