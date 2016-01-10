using eUni.BusinessLogic.IProviders;
using eUni.WebServices.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using AutoMapper;
using Dropbox.Api;
using Dropbox.Api.Files;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Enums;
using eUni.WebServices.Models;
using eUni.WebServices.Models.OutputModels;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/File")]
    public class FileController : ApiController
    {
        private IFileProvider _fileProvider;

        public FileController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        [Route("GetAllFilesByModule")]
        public async Task<IHttpActionResult> GetAllByModule(int? moduleId)
        {

            if (moduleId == null)
            {
                return BadRequest("Module Id not found");
            }

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var fileDtos = _fileProvider.GetByModule(moduleId.Value);
            var fileOutModels = Mapper.Map<List<FileOutModel>>(fileDtos);
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Files for Module "));
            return Content(HttpStatusCode.OK, fileOutModels);
        }

        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(int fileId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            _fileProvider.DeleteFileWithId(fileId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "File deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IHttpActionResult> UploadFile(string filename, [FromBody]byte[] contentFile, int moduleId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            
            string username = TokenHelper.GetFromToken(token, "username");
            string role = TokenHelper.GetFromToken(token, "role");

            string path = "/" + role + "/" + username + "/" + filename;

            FileType fileType;
            var lastOrDefault = filename.Split('.').LastOrDefault();
            if (lastOrDefault != null)
                if (Enum.TryParse(lastOrDefault, out fileType))
                {
                    try
                    {
                        await FileHelper.UploadFile(username, path, contentFile, _fileProvider, filename, fileType, moduleId);
                        return Ok(path);
                    }
                    catch (Exception ex)
                    {
                        Logger.Logger.Instance.LogError(ex);
                        return InternalServerError(ex);
                    }
                }
                else
                    return BadRequest("File type not supported.");

            return BadRequest("Not a file type.");
        }

        [HttpGet]
        [Route("DownloadLink")]
        public async Task<IHttpActionResult> DownloadLink(int moduleId)
        {
            var key = WebConfigurationManager.AppSettings["DropboxToken"];
            var dbx = new DropboxClient(key);

            var files = _fileProvider.GetByModule(moduleId);

            var model = new List<FileViewModel>();

            foreach (var file in files)
            {
                try
                {
                    var downloadLink = await dbx.Sharing.CreateSharedLinkAsync(file.Path, false);

                    var viewModel = new FileViewModel
                    {
                        Filename = file.FileName,
                        Path = downloadLink.Url.Remove(downloadLink.Url.Length - 1) + "1"
                    };
                    model.Add(viewModel);
                }
                catch (Exception ex)
                {
                    Logger.Logger.Instance.LogError(ex);
                    return InternalServerError(ex);
                }
            }
            return Ok(model);
        }
    }
}
