using eUni.BusinessLogic.IProviders;
using eUni.WebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using eUni.BusinessLogic.Providers.DataTransferObjects;
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
    }
}
