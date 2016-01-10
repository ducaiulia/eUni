using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using AutoMapper;
using Dropbox.Api;
using eUni.BusinessLogic.IProviders;
using eUni.WebServices.Helpers;
using eUni.WebServices.Models;
using eUni.WebServices.Models.OutputModels;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/File")]
    public class FileController : ApiController
    {
        private IFileProvider _fileProvider;
        private IStudentHomeworkProvider _studentHomeworkProvider;
        public FileController(IFileProvider fileProvider, IStudentHomeworkProvider studentHomeworkProvider)
        {
            _fileProvider = fileProvider;
            _studentHomeworkProvider = studentHomeworkProvider;
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
        public async Task<IHttpActionResult> UploadFile(string filename, [FromBody] byte[] contentFile, int moduleId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            try
            {
                await FileHelper.UploadFile(token, contentFile, _fileProvider, filename, moduleId);
                return Ok("File Uploaded");
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.LogError(ex);
                return InternalServerError(ex);
            }
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

        [Route("GetFilesByHomeworkIdStudentId")]
        public async Task<IHttpActionResult> GetFilesByHomeworkIdStudentId(int? homeworkId, int? studentId)
        {
            if (homeworkId == null || studentId == null)
            {
                return BadRequest("homework id nad student id not found");
            }

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Files for homework id and student id "));

            var filesDto = _studentHomeworkProvider.GetFilesByStundentIdHomeworkId((int)studentId, (int)homeworkId);
            var fileOutModels = Mapper.Map<List<FileOutModel>>(filesDto);
            return Content(HttpStatusCode.OK, fileOutModels);
        }

        [Route("GetFileById")]
        public async Task<IHttpActionResult> GetFileById(int fileId)
        {
            var file = _fileProvider.GetFileById(fileId);
            if (file != null)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                Logger.Logger.Instance.LogAction(
                    LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get File By Id"));
                return Ok(Mapper.Map<FileOutModel>(file));
            }
            return BadRequest("File Id doesn't exist");
        }
    }
}
