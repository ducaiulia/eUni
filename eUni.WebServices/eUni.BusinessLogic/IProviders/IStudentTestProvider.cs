using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface IStudentTestProvider
    {
        void CreateStudentTest(StudentTestDTO studentTest);
        int GetGradeForStudentWithIdTestId(int studentId, int testId);
    }
}