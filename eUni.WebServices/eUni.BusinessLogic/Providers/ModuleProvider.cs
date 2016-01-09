using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Repository;
using AutoMapper;
using eUni.DataAccess.Domain;

namespace eUni.BusinessLogic.Providers
{
    public class ModuleProvider : IModuleProvider
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IModuleRepository _moduleRepo;

        public ModuleProvider(ICourseRepository courseRepo, IModuleRepository moduleRepo)
        {
            _courseRepo = courseRepo;
            _moduleRepo = moduleRepo;
        }

        public void CreateModule(ModuleDTO dtoMod)
        {
            var module = Mapper.Map<Module>(dtoMod);
            module.Course = _courseRepo.Get(u => u.CourseId == dtoMod.Course.CourseId);
            _moduleRepo.Add(module);
        }

        public ModuleDTO GetById(int modId)
        {
            var module = _moduleRepo.Get(u => u.ModuleId == modId);
            return Mapper.Map<ModuleDTO>(module);
        }

        public void UpdateModule(ModuleDTO mod)
        {
            var module = _moduleRepo.Get(c => c.ModuleId == mod.ModuleId);
            module.Course = _courseRepo.Get(u => u.CourseId == mod.Course.CourseId);
            _moduleRepo.SaveChanges();
        }
    }
}
