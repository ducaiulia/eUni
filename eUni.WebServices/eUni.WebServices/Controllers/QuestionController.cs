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
    [RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {
        private IModuleProvider _moduleProvider;
        private IQuestionProvider _questionProvider;

        public QuestionController(IModuleProvider moduleProvider, IQuestionProvider questionProvider)
        {
            _moduleProvider = moduleProvider;
            _questionProvider = questionProvider;
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(QuestionViewModel question)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            QuestionDTO dtoQuestion = Mapper.Map<QuestionDTO>(question);
            _questionProvider.CreateQuestion(dtoQuestion);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Question created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(int questionId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            _questionProvider.DeleteQuestionWithId(questionId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Question deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

        [Route("GetAllQuestionsByModule")]
        public async Task<IHttpActionResult> GetAllByModule(int? moduleId, int? pageNumber, int? pageSize)
        {
            if (moduleId == null)
            {
                return BadRequest("Module Id not found");
            }

            var filter = new PaginationFilter
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var questionDtos = _questionProvider.GetByModule(moduleId.Value, filter);
            var questionModels = Mapper.Map<List<QuestionViewModel>>(questionDtos);
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Questions for Module "));
            return Content(HttpStatusCode.OK, questionModels);
        }

        [Route("Update")]
        public async Task<IHttpActionResult> Update(QuestionDTO questionDto)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            _questionProvider.UpdateQuestion(questionDto);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Question Updated "));
            return Content(HttpStatusCode.OK, "Updated successfully");
        }
    }
}