using System;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class AnswerProvider : IAnswerProvider
    {
        private IQuestionRepository _questionRepository;
        private IAnswerRepository _answerRepository;

        public AnswerProvider(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public void CreateAnswer(AnswerDTO dtoAnswer)
        {
            var answer = Mapper.Map<Answer>(dtoAnswer);
            var question = _questionRepository.Get(u => u.QuestionId == dtoAnswer.AnswerId);
            if (question == null)
            {
                throw new Exception("Question was not found");
            }
            question.Answers.Add(answer);
            _answerRepository.Add(answer);

            _questionRepository.SaveChanges();
        }

        public void DeleteAnswerWithId(int answerId)
        {
            var answer = _answerRepository.Get(t => t.AnswerId == answerId);

            if (answer == null)
            {
                throw new Exception("Question was not found");
            }

            _answerRepository.Remove(answer);
        }
    }
}