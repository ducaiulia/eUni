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

        public QuestionProvider(IQuestionRepository questionRepository, IModuleRepository moduleRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _moduleRepository = moduleRepository;
            _answerRepository = answerRepository;
        }

        public QuestionDTO GetQuestionById(int questionId)
        {
            var module = _questionRepository.Get(u => u.QuestionId == questionId);
            return Mapper.Map<QuestionDTO>(module);
        }

        public void CreateQuestion(QuestionDTO dtoQuestion)
        {
            var question = Mapper.Map<Question>(dtoQuestion);
            question.Module = _moduleRepository.Get(u => u.ModuleId == dtoQuestion.Module.ModuleId);
            _questionRepository.Add(question);
        }

        public void UpdateQuestion(QuestionDTO dtoQuestion)
        {
            var question = Mapper.Map<Question>(dtoQuestion);
            question.Module = _moduleRepository.Get(u => u.ModuleId == dtoQuestion.Module.ModuleId);
            _questionRepository.SaveChanges();
        }
    }
}
