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
        private IStudentTestProvider _studentTestProvider;

        public TestController(IStudentTestProvider studentTestProvider,IModuleProvider moduleProvider, IQuestionProvider questionProvider, ITestProvider testProvider)
        {
            _moduleProvider = moduleProvider;
            _questionProvider = questionProvider;
            _testProvider = testProvider;
            _studentTestProvider = studentTestProvider;
        }

        [Route("Add")]
        [@Authorize("teacher")]
        public async Task<IHttpActionResult> Add(TestViewModel test)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            TestDTO dtoTest = Mapper.Map<TestDTO>(test);
            _testProvider.CreateTest(dtoTest);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Test created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("Remove")]
        [@Authorize("teacher")]
        public async Task<IHttpActionResult> Remove(int testId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            _testProvider.DeleteTestWithId(testId);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Test deleted"));
            return Content(HttpStatusCode.OK, "Deleted successfully");
        }

        [Route("UpdateGrade")]
        public async Task<IHttpActionResult> UpdateGrade(StudentTestDTO studentTest)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var studentTestDTO = Mapper.Map<StudentTestDTO>(studentTest);
            //hwDTO.Files.Add(_fileRepo.Get(f=>f.Id == fileId));
            _studentTestProvider.CreateStudentTest(studentTestDTO);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Grade updated"));
            return Content(HttpStatusCode.OK, "Updated successfully");
        }

        [Route("GetAllTestsByModule")]
        public async Task<IHttpActionResult> GetAllByModule(int? moduleId)
        {

            if (moduleId == null)
            {
                return BadRequest("Module Id not found");
            }

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var testDtos = _testProvider.GetByModule(moduleId.Value);
            var testModels = Mapper.Map<List<TestViewModel>>(testDtos);
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Tests for Module "));
            return Content(HttpStatusCode.OK, testModels);
        }

        [Route("GetAllTestsByModuleWithPagination")]
        public async Task<IHttpActionResult> GetAllByModuleWithPagination(int? moduleId, int? pageNumber, int? pageSize)
        {

            if (moduleId == null)
            {
                return BadRequest("Module Id not found");
            }

            var filter = new PaginationFilter()
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();

            var testDtos = _testProvider.GetByModuleWithPagination(moduleId.Value, filter);
            var testModels = Mapper.Map<List<TestViewModel>>(testDtos);
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Tests for Module "));
            return Content(HttpStatusCode.OK, testModels);
        }

        [Route("GetAllQuestionsByTestId")]
        public async Task<IHttpActionResult> GetAllQuestionsByTestId(int? testId)
        {
            if (testId == null)
            {
                return BadRequest("Test Id not found");
            }

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var testDto = _testProvider.GetByTestId(testId.Value);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Questions for Test"));
            return Content(HttpStatusCode.OK, testDto);
        }

        [Route("GetAllQuestionsByTestIdWithPagination")]
        public async Task<IHttpActionResult> GetAllQuestionsByTestIdWithPagination(int testId, int? pageNumber, int? pageSize)
        {
            if (testId == null)
            {
                return BadRequest("Test Id not found");
            }

            var filter = new PaginationFilter()
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var testDto = _testProvider.GetByTestId(testId);
            var questions = testDto.Questions.OrderBy(x => x.QuestionId)
                                .Skip((filter.PageNumber - 1) * filter.PageSize)
                                .Take(filter.PageSize).ToList();
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"), "Get All Questions for Test"));
            return Content(HttpStatusCode.OK, questions);
        }
    }
}