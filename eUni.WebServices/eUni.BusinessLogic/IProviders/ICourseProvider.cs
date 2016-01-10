using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface ICourseProvider
    {
        void CreateCourse(CourseDTO dtoCourse);
        void DeleteCourseWithId(int courseId);
        CourseDTO GetByCourseCode(string courseCode);
        void UpdateCourse(CourseDTO course);
        List<CourseDTO> GetAll();
    }
}
