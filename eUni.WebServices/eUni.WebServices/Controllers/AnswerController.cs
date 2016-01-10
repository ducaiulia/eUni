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
    [RoutePrefix("api/Answer")]
    public class AnswerController : ApiController
    {
        private IAnswerProvider _answerProvider;

        public AnswerController(IAnswerProvider answerProvider)
        {
            _answerProvider = answerProvider;
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(AnswerViewModel answer)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            AnswerDTO dtoAnswer = Mapper.Map<AnswerDTO>(answer);
            _answerProvider.CreateAnswer(dtoAnswer);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Answer created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(int answerId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            _answerProvider.DeleteAnswerWithId(answerId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Answer deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

        [Route("IsCorrect")]
        public async Task<IHttpActionResult> IsCorrect(int answerId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Answer checked"));
            return Content(HttpStatusCode.OK, _answerProvider.IsCorrectAnswerWithId(answerId));
        }
    }
}