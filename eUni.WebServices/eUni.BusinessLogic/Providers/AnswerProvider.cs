﻿using System;
using System.Collections.Generic;
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
            var question = _questionRepository.Get(u => u.QuestionId == dtoAnswer.QuestionId);
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

        public bool IsCorrectAnswerWithId(int answerId)
        {
            var answer = _answerRepository.Get(t => t.AnswerId == answerId);
            return answer.IsCorrect;
        }

        public void UpdateAnswer(AnswerDTO dtoAnswer)
        {
            var answer = _answerRepository.Get(t => t.AnswerId == dtoAnswer.AnswerId);
            answer.Text = dtoAnswer.Text;
            answer.IsCorrect = dtoAnswer.IsCorrect;
            _answerRepository.SaveChanges();
        }

        public void CreateAnswers(IEnumerable<AnswerDTO> dtoAnswers)
        {
            foreach(var a in dtoAnswers)
                CreateAnswer(a);
        }
    }
}