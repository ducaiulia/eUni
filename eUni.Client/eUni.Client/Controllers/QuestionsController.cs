using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EUni_Client.Models;
using EUni_Client.Services;

namespace EUni_Client.Controllers
{
    public class QuestionsController : Controller
    {
        // GET: Questions
        public async Task<ActionResult> Index(TestViewModel vm)
        {
            var apiService = Session.GetApiService();

            var testQuestions =  await apiService.GetAsync<IList<QuestionViewModel>, int>("/Test/GetAllQuestionsByTestId", "testId", vm.TestId);

            var moduleQuestions = await apiService.GetAsync<IList<QuestionViewModel>, int>("/Question/GetAllByModuleId", "moduleId", vm.ModuleId);

            var unassigned = moduleQuestions.Where(q => !testQuestions.Any(t => t.QuestionId.Equals(q.QuestionId)));

            return View(new QuestionResultsViewModel {TestId = vm.TestId, ModuleQuestions = unassigned.ToList(), TestQuestions = testQuestions});
        }

        [HttpPost]
        public async Task AddQuestion(int questionId, int testId)
        {
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<string, object>("/Question/AssignQuestionToTest", new { QuestionId = questionId, TestId = testId});
        }
    }
}