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

        public FileProvider(IModuleRepository moduleRepo, IFileRepository fileRepo)
        {
            _moduleRepo = moduleRepo;
            _fileRepo = fileRepo;
        }

        public bool SaveUploadedFilePath(FileDTO fileDTO)
        {
            var module = _moduleRepo.Get(m => m.ModuleId.Equals(fileDTO.ModuleId));
            if (module == null)
                return false;

            var file = Mapper.Map<File>(fileDTO);

            file.Module = module;

            _fileRepo.Add(file);
            _fileRepo.SaveChanges();

            module.Files.Add(file);
            _moduleRepo.SaveChanges();

            return true;
        }

        public List<FileDTO> GetByModule(int moduleId)
        {
            var files = _fileRepo.GetAll().Where(u => u.Module.ModuleId == moduleId);
            var fileDtos = Mapper.Map<List<FileDTO>>(files);
            return fileDtos;
        }

        public void DeleteFileWithId(int fileId)
        {
            var test = _fileRepo.Get(t => t.Id == fileId);
            _fileRepo.Remove(test);
        }

    }
}
