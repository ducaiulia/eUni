using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface IQuestionProvider
    {
        void CreateQuestion(QuestionDTO dtoQuestion);
        void UpdateQuestion(QuestionDTO dtoQuestion);
        QuestionDTO GetQuestionById(int id);
    }
}
