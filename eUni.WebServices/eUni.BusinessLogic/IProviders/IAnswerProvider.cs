using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface IAnswerProvider
    {
        void CreateAnswer(AnswerDTO dtoAnswer);
        void DeleteAnswerWithId(int answerId);
        bool IsCorrectAnswerWithId(int answerId);
    }
}