﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface IQuestionProvider
    {
        int CreateQuestion(QuestionDTO dtoQuestion);
        void UpdateQuestion(QuestionDTO dtoQuestion);
        void DeleteQuestionWithId(int id);
        QuestionDTO GetQuestionById(int id);
        List<QuestionDTO> GetByModule(int ModuleId, PaginationFilter filter);
        List<QuestionDTO> GetAllByModuleId(int moduleId);
        void AssignQuestionToTest(int questionId, int testId);
    }
}
