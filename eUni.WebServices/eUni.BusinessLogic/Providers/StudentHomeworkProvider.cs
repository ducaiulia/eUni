using eUni.BusinessLogic.IProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Repository;
using AutoMapper;
using eUni.DataAccess.Domain;

namespace eUni.BusinessLogic.Providers
{
    public class StudentHomeworkProvider : IStudentHomeworkProvider
    {
        private IUserRepository _userRepo;
        private IHomeworkRepository _homeworkRepo;
        private IStudentHomeworkRepository _studentHWRepository;

        public StudentHomeworkProvider(IHomeworkRepository homeworkRepo,IUserRepository userRepo, IStudentHomeworkRepository studentHWRepository)
        {
            _userRepo = userRepo;
            _studentHWRepository = studentHWRepository;
            _homeworkRepo = homeworkRepo;
        }

        public void CreateStudentHomework(StudentHomeworkDTO hw)
        {
            var c = Mapper.Map<StudentHomework>(hw);
            c.DomainUser = _userRepo.Get(u => u.DomainUserId == hw.StudentId);
            c.Homework = _homeworkRepo.Get(h=>h.HomeworkId == hw.HomeworkId);
            hw.Files.ForEach(f=> c.Files.Add(Mapper.Map<File>(f)));
            _studentHWRepository.Add(c);
        }
    }
}
