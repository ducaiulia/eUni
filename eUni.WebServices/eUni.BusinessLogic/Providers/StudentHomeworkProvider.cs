using eUni.BusinessLogic.IProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Repository;
using eUni.DataAccess.Domain;

namespace eUni.BusinessLogic.Providers
{
    public class StudentHomeworkProvider : IStudentHomeworkProvider
    {
        private IUserRepository _userRepo;
        private IHomeworkRepository _homeworkRepo;
        private IStudentHomeworkRepository _studentHWRepository;
        private IModuleRepository _moduleRepository;
        private IFileRepository _fileRepository;

        public StudentHomeworkProvider(IFileRepository fileRepository, IHomeworkRepository homeworkRepo, IUserRepository userRepo, IStudentHomeworkRepository studentHWRepository, IModuleRepository moduleRepository)
        {
            _userRepo = userRepo;
            _studentHWRepository = studentHWRepository;
            _homeworkRepo = homeworkRepo;
            _moduleRepository = moduleRepository;
            _fileRepository = fileRepository;
        }

        public void CreateStudentHomework(StudentHomeworkDTO hw)
        {
            var sHw = Exists(hw.StudentId, hw.HomeworkId);
            if (sHw == null)
            {
                var newHw = new StudentHomework();
                newHw.HomeworkId = hw.HomeworkId;
                newHw.Grade = hw.Grade;
                newHw.Files = new List<File>();

                hw.Files.ForEach(f =>
                {
                    var file = _fileRepository.Get(fil => fil.Id == f.Id);
                    file.StudentHomework = _studentHWRepository.Get(s => s.DomainUserId == hw.StudentId && s.HomeworkId == hw.HomeworkId);
                    newHw.Files.Add(file);
                    _fileRepository.SaveChanges();
                });
                newHw.DomainUser = _userRepo.Get(u => u.DomainUserId == hw.StudentId);
                newHw.Homework = _homeworkRepo.Get(h => h.HomeworkId == hw.HomeworkId);
                _studentHWRepository.Add(newHw);
                _studentHWRepository.SaveChanges();
            }
            else
            {
                hw.Files.ForEach(f =>
                {
                    var file = _fileRepository.Get(fil => fil.Id == f.Id);
                    file.Module = _moduleRepository.Get(m => m.ModuleId.Equals(f.ModuleId));
                    file.StudentHomework = _studentHWRepository.Get(s => s.DomainUserId == hw.StudentId && s.HomeworkId == hw.HomeworkId);
                    _fileRepository.SaveChanges();

                });
                _studentHWRepository.SaveChanges();
            }
        }

        public void Update(int userId, int hwId)
        {
            var sHw = Exists(userId, hwId);
            if (sHw != null)
            {
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

        public List<FileDTO> GetFilesByStundentIdHomeworkId(int stundentId, int homeworkId)
        {
            var sH = _studentHWRepository.GetAll().Where(current => current.DomainUserId == stundentId && current.HomeworkId == homeworkId);
            List<File> files = null;
            foreach (var x in sH)
            {
                files = x.Files;
                break;
            }

            List<FileDTO> filesDTO = new List<FileDTO>();
            foreach (File file in files)
            {
                filesDTO.Add(new FileDTO()
                {
                    FileName = file.FileName,
                    FileType = file.FileType,
                    ModuleId = file.Module.ModuleId,
                    Path = file.Path
                });
            }

            return filesDTO;
        }
    }
}
