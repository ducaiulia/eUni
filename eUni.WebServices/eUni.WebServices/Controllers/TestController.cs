using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Helpers;
using eUni.WebServices.Models;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        private IModuleProvider _moduleProvider;
        private IQuestionProvider _questionProvider;
        private ITestProvider _testProvider;

        public TestController(IModuleProvider moduleProvider, IQuestionProvider questionProvider, ITestProvider testProvider)
        {
            _moduleProvider = moduleProvider;
            _questionProvider = questionProvider;
            _testProvider = testProvider;
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(TestViewModel test)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            TestDTO dtoTest = Mapper.Map<TestDTO>(test);
            _testProvider.CreateTest(dtoTest);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Test created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }
    }
}