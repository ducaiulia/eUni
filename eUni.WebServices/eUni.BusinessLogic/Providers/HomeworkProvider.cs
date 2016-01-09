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

        public HomeworkProvider(IHomeworkRepository homeworkRepo, IModuleRepository moduleRepo)
        {
            _homeworkRepo = homeworkRepo;
            _moduleRepo = moduleRepo;
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
            return Mapper.Map<HomeworkDTO>(hw);
        }

        public void UpdateHomework(HomeworkDTO hw)
        {
            var homew = _homeworkRepo.Get(c => c.HomeworkId == hw.HomeworkId);
            homew.Module = _moduleRepo.Get(u => u.ModuleId == hw.Module.ModuleId);
            _homeworkRepo.SaveChanges();
        }
    }
}
