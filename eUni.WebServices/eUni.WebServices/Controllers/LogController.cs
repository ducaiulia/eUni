using eUni.BusinessLogic.IProviders;
using eUni.WebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace eUni.WebServices.Controllers
{
    [RoutePrefix("api/Log")]
    public class LogController : ApiController
    {
        private ILogProvider _logProvider;

        public LogController(ILogProvider logProvider)
        {
            _logProvider = logProvider;
        }


        [Route("Get")]
        public async Task<IHttpActionResult> Get()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            var res = _logProvider.GetAllLogs();

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Homework created"));
            return Ok(res);
        }
    }
}
