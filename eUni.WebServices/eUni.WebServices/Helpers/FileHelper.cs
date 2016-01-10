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
        public static async Task<int> UploadFile(string token, byte[] contentFile, IFileProvider fileProvider,
            string filename, int moduleId = -1, int hwId = -1)
        {

            string username = TokenHelper.GetFromToken(token, "username");
            string role = TokenHelper.GetFromToken(token, "role");

            string path = "/" + role + "/" + username + "/" + filename;

            var key = WebConfigurationManager.AppSettings["DropboxToken"];
            var dbx = new DropboxClient(key);

            FileType fileType;
            var lastOrDefault = filename.Split('.').LastOrDefault();
            if (lastOrDefault != null)
                if (Enum.TryParse(lastOrDefault, out fileType))
                {
                    using (var memoryStream = new MemoryStream(contentFile))
                    {
                        try
                        {
                            await dbx.Files.UploadAsync(
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
                            FileName = filename
                        };
                        var fileId = fileProvider.SaveUploadedFilePath(input, moduleId, hwId);
                        if (fileId < 0)
                            throw new Exception("Cannot save file path");
                        else
                            return fileId;
                    }
                }
                else
                    throw new Exception("File tye not supported");
            else
                throw new Exception("Not a file type");
        }
    }
}