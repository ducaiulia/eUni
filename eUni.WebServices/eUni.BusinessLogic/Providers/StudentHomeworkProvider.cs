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
            foreach(File file in files)
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
