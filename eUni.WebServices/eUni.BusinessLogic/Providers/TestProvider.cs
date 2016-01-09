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
    class TestProvider : ITestProvider
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IQuestionRepository _questionProvider;
        private readonly ITestRepository _testRepository;

        public TestProvider(IModuleRepository moduleRepository, IQuestionRepository questionProvider, ITestRepository testRepository)
        {
            _moduleRepository = moduleRepository;
            _questionProvider = questionProvider;
            _testRepository = testRepository;
        }

        public void CreateTest(TestDTO dtoTest)
        {
            var test = Mapper.Map<Test>(dtoTest);
            test.Module = _moduleRepository.Get(u => u.ModuleId == dtoTest.Module.ModuleId);
            _testRepository.Add(test);
        }

        public TestDTO GetByTestId(int testId)
        {
            var test = _testRepository.Get(u => u.TestId == testId);
            return Mapper.Map<TestDTO>(test);
        }

        public void UpdateTest(TestDTO dtoTest)
        {
            var test = Mapper.Map<Test>(dtoTest);
            test.Module = _moduleRepository.Get(u => u.ModuleId == dtoTest.Module.ModuleId);
            _testRepository.SaveChanges();
        }
    }
}
