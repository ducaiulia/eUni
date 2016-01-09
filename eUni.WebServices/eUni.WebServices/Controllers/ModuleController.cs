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
    public class ModuleController : ApiController
    {
        private IModuleProvider _moduleProvider;
        private ICourseProvider _courseProvider;
        
        public ModuleController(IModuleProvider moduleProvider, ICourseProvider courseProvider)
        {
            _moduleProvider = moduleProvider;
            _courseProvider = courseProvider;
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(ModuleViewModel mod)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            ModuleDTO dtoMod = Mapper.Map<ModuleDTO>(mod);
            dtoMod.Course = _courseProvider.GetByCourseCode(mod.Course.CourseCode);
            _moduleProvider.CreateModule(dtoMod);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Module created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }
    }
}
