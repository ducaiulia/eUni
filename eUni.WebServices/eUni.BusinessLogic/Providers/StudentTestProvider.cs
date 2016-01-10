using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class StudentTestProvider : IStudentTestProvider
    {
        private IUserRepository _userRepo;
        private ITestRepository _testRepo;
        private IStudentTestRepository _studentTestRepository;

        public StudentTestProvider(ITestRepository testRepo, IUserRepository userRepo, IStudentTestRepository studentTestRepository)
        {
            _userRepo = userRepo;
            _studentTestRepository = studentTestRepository;
            _testRepo = testRepo;
        }
        public void CreateStudentTest(StudentTestDTO studentTest)
        {
            var c = Mapper.Map<StudentTest>(studentTest);
            c.DomainUser = _userRepo.Get(u => u.DomainUserId == studentTest.StudentId);
            c.Test = _testRepo.Get(h => h.TestId == studentTest.TestId);
            c.Grade = studentTest.Grade;
            _studentTestRepository.Add(c);
}
    }
}