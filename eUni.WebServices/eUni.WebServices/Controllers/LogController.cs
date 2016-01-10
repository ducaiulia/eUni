using eUni.BusinessLogic.IProviders;
using eUni.WebServices.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using eUni.BusinessLogic.Providers.DataTransferObjects;

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

            var res = _logProvider.GetAllLogs(false, new PaginationFilter());

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Homework created"));
            return Ok(res);
        }

        [Route("GetWithPagination")]
        public async Task<IHttpActionResult> GetWithPagination(int? pageNumber, int? pageSize)
        {
            var filter = new PaginationFilter
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            var res = _logProvider.GetAllLogs(true, filter);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Homework created"));
            return Ok(res);
        }

    }
}
