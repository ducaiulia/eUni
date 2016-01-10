using eUni.BusinessLogic.IProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using AutoMapper;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;

namespace eUni.BusinessLogic.Providers
{
    public class HomeworkProvider : IHomeworkProvider
    {
        private readonly IHomeworkRepository _homeworkRepo;
        private readonly IModuleRepository _moduleRepo;
        private readonly IStudentHomeworkRepository _studentHomeworkRepo;
        public HomeworkProvider(IHomeworkRepository homeworkRepo, IModuleRepository moduleRepo, IStudentHomeworkRepository studentHomeworkRepo)
        {
            _homeworkRepo = homeworkRepo;
            _moduleRepo = moduleRepo;
            _studentHomeworkRepo = studentHomeworkRepo;
        }

        public void CreateHomework(HomeworkDTO dtoHw)
        {
            var h = Mapper.Map<Homework>(dtoHw);
            h.Module = _moduleRepo.Get(u => u.ModuleId == dtoHw.Module.ModuleId);
            _homeworkRepo.Add(h);
        }

        public HomeworkDTO GetById(int hwId)
        {
            var hw = _homeworkRepo.Get(u => u.HomeworkId == hwId);
            var res = Mapper.Map<HomeworkDTO>(hw);
            res.Module = Mapper.Map<ModuleDTO>(hw.Module);
            return res;
        }

        public void UpdateHomework(HomeworkDTO hw)
        {
            var homew = _homeworkRepo.Get(c => c.HomeworkId == hw.HomeworkId);
            if (homew == null)
                throw new Exception("Homework not found.");

            homew.Text = hw.Text;
            homew.Score = hw.Score;
            
            _homeworkRepo.SaveChanges();
        }

        public void DeleteHomeworkWithId(int hwId)
        {
            var hw = _homeworkRepo.Get(c => c.HomeworkId == hwId);
            _homeworkRepo.Remove(hw);
        }

        /// <summary>
        /// Get all homeworks for a given module.
        /// </summary>
        /// <param name="moduleId">the given module.</param>
        /// <returns></returns>
        public List<HomeworkDTO> GetHomeworksByModuleId(int moduleId)
        {
            var allByModuleId = _homeworkRepo.GetAll().Where(x => x.Module.ModuleId == moduleId);
            var allByModuleIdDTO = Mapper.Map<List<HomeworkDTO>>(allByModuleId);
            return allByModuleIdDTO;

        }

        public List<HomeworkDTO> GetHomeworkdsByModuleIdStudentId(int studentId, int moduleId)
        {
            var allByModuleId = _homeworkRepo.GetAll().Where(x => x.Module.ModuleId == moduleId);
            var allStudentHomework = _studentHomeworkRepo.GetAll().Where(x => x.DomainUserId == studentId);
            List<Homework> homeworks = new List<Homework>();
            foreach (var currentHomework in allByModuleId)
            {
                var result = allStudentHomework.FirstOrDefault(current => current.HomeworkId == currentHomework.HomeworkId);
                if (result == null)
                {
                    continue;
                }
                homeworks.Add(currentHomework);
            }

            var homeworksDTO = Mapper.Map<List<HomeworkDTO>>(homeworks);
            return homeworksDTO;
        }
    }
}
