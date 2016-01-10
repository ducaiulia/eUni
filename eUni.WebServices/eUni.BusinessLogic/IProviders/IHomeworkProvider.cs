using eUni.BusinessLogic.Providers.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.IProviders
{
    public interface IHomeworkProvider
    {
        void CreateHomework(HomeworkDTO dtoHw);
        HomeworkDTO GetById(int hwId);
        void UpdateHomework(HomeworkDTO hw);
        void DeleteHomeworkWithId(int hwId);
        List<HomeworkDTO> GetHomeworksByModuleId(int moduleId);
        List<HomeworkDTO> GetHomeworkdsByModuleIdStudentId(int studentId, int moduleId);
        Task<List<UploadedHomeworksDTO>> GetUploadedHW(int hwId);
        void SetGradeUploadedHW(GradeToHomeworkDTO model);
    }
}
