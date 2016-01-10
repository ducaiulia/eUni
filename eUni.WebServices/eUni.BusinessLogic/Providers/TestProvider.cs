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
    public class TestProvider : ITestProvider
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
            test.Module = _moduleRepository.Get(u => u.ModuleId == dtoTest.ModuleId);
            _testRepository.Add(test);
        }

        public void DeleteTestWithId(int testId)
        {
            var test = _testRepository.Get(t => t.TestId == testId);
            _testRepository.Remove(test);
        }

        public List<TestDTO> GetByModule(int ModuleId)
        {
            var tests = _testRepository.GetAll().Where(x => x.Module.ModuleId == ModuleId);
            var testDtos = Mapper.Map<List<TestDTO>>(tests);
            return testDtos;
        }

        public TestDTO GetByTestId(int testId)
        {
            var test = _testRepository.Get(u => u.TestId == testId);
            return Mapper.Map<TestDTO>(test);
        }

        public void UpdateTest(TestDTO dtoTest)
        {
            var test = Mapper.Map<Test>(dtoTest);
            _testRepository.SaveChanges();
        }

        
    }
}
