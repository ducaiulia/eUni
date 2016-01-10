using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface ITestProvider
    {
        void CreateTest(TestDTO dtoTest);
        TestDTO GetByTestId(int testId);
        void UpdateTest(TestDTO dtoTest);
        void DeleteTestWithId(int testId);
        List<TestDTO> GetByModule(int ModuleId);
    }
}
