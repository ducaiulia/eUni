using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Helpers;
using eUni.WebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/Homework")]
    public class HomeworkController : ApiController
    {
        private IModuleProvider _moduleProvider;
        private IHomeworkProvider _homeworkProvider;
        private IStudentHomeworkProvider _studentHWProvider;
        private IFileProvider _fileProvider;

        public HomeworkController(IFileProvider fileProvider, IStudentHomeworkProvider studentHWProvider, IModuleProvider moduleProvider, IHomeworkProvider homeworkProvider)
        {
            _moduleProvider = moduleProvider;
            _homeworkProvider = homeworkProvider;
            _studentHWProvider = studentHWProvider;
            _fileProvider = fileProvider;
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(HomeworkViewModel hw)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            HomeworkDTO dtoHw = Mapper.Map<HomeworkDTO>(hw);
            dtoHw.Module = _moduleProvider.GetById(hw.ModuleId);
            _homeworkProvider.CreateHomework(dtoHw);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Homework created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("UploadHomeworkAnswer")]
        public async Task<IHttpActionResult> UploadHomeworkAnswer(StudentHomeworkViewModel hw)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            //var fileId = FileHelper.UploadFile();
            var hwDTO = Mapper.Map<StudentHomeworkDTO>(hw);
            //hwDTO.Files.Add(_fileRepo.Get(f=>f.Id == fileId));
            _studentHWProvider.CreateStudentHomework(hwDTO);
            
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Homework submitted"));
            return Content(HttpStatusCode.OK, "Uploaded successfully");
        }

        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(int hwId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            _homeworkProvider.DeleteHomeworkWithId(hwId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Homework deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

        [Route("Update")]
        public async Task<IHttpActionResult> Update(HomeworkViewModel hw)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            try
            {
                _homeworkProvider.UpdateHomework(Mapper.Map<HomeworkDTO>(hw));
                Logger.Logger.Instance.LogAction(
                    LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Homework updated"));
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.LogError(ex);
                return BadRequest(ex.Message);
            }
        }

        [Route("AllHomoworksByModulId")]
        public async Task<IHttpActionResult> GetHomeworksByModuleId( int moduleId )
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            try
            {
                var homeworks = _homeworkProvider.GetHomeworksByModuleId(moduleId);
                var homeworksViewModels = Mapper.Map<List<HomeworkViewModel>>(homeworks);
                Logger.Logger.Instance.LogAction(
                    LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "AllHomoworksByModulId"));

                return Content(HttpStatusCode.OK, homeworksViewModels);
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.LogError(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
