using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class QuestionProvider : IQuestionProvider
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ITestRepository _testRepository;

        public QuestionProvider(IQuestionRepository questionRepository, IModuleRepository moduleRepository, IAnswerRepository answerRepository, ITestRepository testRepository)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _moduleRepository = moduleRepository;
            _answerRepository = answerRepository;
        }

        public void DeleteQuestionWithId(int id)
        {
            var question = _questionRepository.Get(t => t.QuestionId == id);

            if (question == null)
            {
                throw new Exception("Question was not found");
            }

            _questionRepository.Remove(question);
        }

        public QuestionDTO GetQuestionById(int questionId)
        {
            var module = _questionRepository.Get(u => u.QuestionId == questionId);
            return Mapper.Map<QuestionDTO>(module);
        }

        public List<QuestionDTO> GetByModule(int ModuleId, PaginationFilter filter)
        {
            var questions = _questionRepository.GetAll().Where(x => x.Module.ModuleId == ModuleId).OrderBy(x => x.QuestionId)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
            var questionsDtos = Mapper.Map<List<QuestionDTO>>(questions);
            return questionsDtos;
        }

        public void CreateQuestion(QuestionDTO dtoQuestion)
        {
            var question = Mapper.Map<Question>(dtoQuestion);
            question.Module = _moduleRepository.Get(u => u.ModuleId == dtoQuestion.ModuleId);
            var test = _testRepository.Get(u => u.TestId == dtoQuestion.TestId);
            test.Questions.Add(question);
            _questionRepository.Add(question);

            _testRepository.SaveChanges();
        }

        public void UpdateQuestion(QuestionDTO dtoQuestion)
        {
            var question = _questionRepository.Get(u => u.QuestionId == dtoQuestion.QuestionId);
            question.Score = dtoQuestion.Score;
            question.Text = dtoQuestion.Text;
            _questionRepository.SaveChanges();
        }
    }
}
