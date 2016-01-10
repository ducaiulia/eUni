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

    [RoutePrefix("api/Module")]
    public class ModuleController : ApiController
    {
        private IModuleProvider _moduleProvider;
        private ICourseProvider _courseProvider;
        private IFileProvider _fileProvider;

        public ModuleController(IModuleProvider moduleProvider, ICourseProvider courseProvider, IFileProvider fileProvider)
        {
            _moduleProvider = moduleProvider;
            _courseProvider = courseProvider;
            _fileProvider = fileProvider;
        }


        [Route("GetByCourse")]
        public async Task<IHttpActionResult> GetByCourse(int? courseId)
        {
            if (courseId == null)
            {
                return BadRequest("Course Id Not Found");
            }
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var moduleDtos = _moduleProvider.GetByCourse(courseId.Value);
            var moduleViewModels = Mapper.Map<List<ModuleViewModel>>(moduleDtos);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get Modules By Course"));
            return Content(HttpStatusCode.OK, moduleViewModels);
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(ModuleViewModel mod)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            ModuleDTO dtoMod = Mapper.Map<ModuleDTO>(mod);
            dtoMod.Course = _courseProvider.GetByCourseCode(mod.CourseCode);
            _moduleProvider.CreateModule(dtoMod);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Module created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(int moduleId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            _moduleProvider.DeleteModuleWithId(moduleId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Module deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

        [Route("GetFiles")]
        public async Task<IHttpActionResult> GetFiles(int modId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            
            var filesDTO = _fileProvider.GetByModule(modId);
            List<FileViewModel> res = new List<FileViewModel>();
            filesDTO.ForEach(f => res.Add(Mapper.Map<FileViewModel>(f)));
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Module created"));
            return Ok(res);
        }
    }
}
