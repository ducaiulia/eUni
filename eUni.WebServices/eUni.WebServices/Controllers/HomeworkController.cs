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

        public HomeworkController(IModuleProvider moduleProvider, IHomeworkProvider homeworkProvider)
        {
            _moduleProvider = moduleProvider;
            _homeworkProvider = homeworkProvider;
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
    }
}
