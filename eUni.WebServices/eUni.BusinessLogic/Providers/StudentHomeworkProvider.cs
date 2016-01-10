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
        private IModuleRepository _moduleRepository;

        public StudentHomeworkProvider(IHomeworkRepository homeworkRepo,IUserRepository userRepo, IStudentHomeworkRepository studentHWRepository, IModuleRepository moduleRepository)
        {
            _userRepo = userRepo;
            _studentHWRepository = studentHWRepository;
            _homeworkRepo = homeworkRepo;
            _moduleRepository = moduleRepository;
        }

        public void CreateStudentHomework(StudentHomeworkDTO hw)
        {
            var newHw = new StudentHomework();
            newHw.HomeworkId = hw.HomeworkId;
            newHw.Grade = hw.Grade;
            newHw.Files = new List<File>();

            hw.Files.ForEach(f =>
            {
                newHw.Files.Add(new File
                {
                    FileName = f.FileName,
                    FileType = f.FileType,
                    Id = f.Id,
                    Module = _moduleRepository.Get(m => m.ModuleId.Equals(f.ModuleId)),
                    Path = f.Path
                });
            });

            newHw.DomainUser = _userRepo.Get(u => u.DomainUserId == hw.StudentId);
            newHw.Homework = _homeworkRepo.Get(h => h.HomeworkId == hw.HomeworkId);
            
            try
            {
                if (Exists(hw.StudentId, hw.HomeworkId) != null)
                {
                    UpdateGrade(hw.StudentId, hw.HomeworkId, hw.Grade);
                }
                else
                {
                    _studentHWRepository.Add(newHw);
                    _studentHWRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void UpdateGrade(int userId, int hwId, int grade)
        {
            var sHw = Exists(userId, hwId);
            if (sHw != null)
            {
                sHw.Grade = grade;
                _studentHWRepository.SaveChanges();
            }
            else
                throw new Exception("StudentHomework doesn't exist");
        }

        public StudentHomework Exists(int userId, int hwId)
        {
            var studentHw = _studentHWRepository.Get(s => s.DomainUserId.Equals(userId) && s.HomeworkId.Equals(hwId));
            return studentHw;
        }
    }
}
