using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;
using WebGrease.Css.Extensions;

namespace eUni.BusinessLogic.Providers
{
    public class FileProvider : IFileProvider
    {
        private readonly IModuleRepository _moduleRepo;
        private readonly IFileRepository _fileRepo;
        private readonly IHomeworkRepository _hwRepo;

        public FileProvider(IModuleRepository moduleRepo, IFileRepository fileRepo, IHomeworkRepository hwRepo)
        {
            _moduleRepo = moduleRepo;
            _fileRepo = fileRepo;
            _hwRepo = hwRepo;
        }

        public int SaveUploadedFilePath(FileDTO fileDTO, int moduleId = -1, int hwId = -1)
        {
            if (moduleId > 0)
            {
                var module = _moduleRepo.Get(m => m.ModuleId.Equals(moduleId));
                if (module == null)
                    return -1;
                var file = Mapper.Map<File>(fileDTO);

                file.Module = module;

                _fileRepo.Add(file);
                _fileRepo.SaveChanges();

                module.Files.Add(file);
                _moduleRepo.SaveChanges();
                return file.Id;
            }

            if (hwId > 0)
            {
                var hw = _hwRepo.Get(h => h.HomeworkId.Equals(hwId));
                if (hw == null)
                    return -1;

                var file = Mapper.Map<File>(fileDTO);

                file.Module = hw.Module;
                
                _fileRepo.Add(file);
                _fileRepo.SaveChanges();
                return file.Id;
            }
            return -1;
        }

        public List<FileDTO> GetByModule(int moduleId)
        {
            var files = _fileRepo.GetAll().Where(u => u.Module.ModuleId == moduleId);
            var fileDtos = Mapper.Map<List<FileDTO>>(files);
            return fileDtos;
        }

        public FileDTO GetFileById(int fileId)
        {
            var temp = _fileRepo.Get(f => f.Id.Equals(fileId));

            return Mapper.Map<FileDTO>(temp);
        }

        public void DeleteFileWithId(int fileId)
        {
            var test = _fileRepo.Get(t => t.Id == fileId);
            _fileRepo.Remove(test);
        }

    }
}
