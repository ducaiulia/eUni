using AutoMapper;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;
using eUni.BusinessLogic.IProviders;

namespace eUni.BusinessLogic.Providers
{
    public class CourseProvider : ICourseProvider
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IUserRepository _userRepo;

        public CourseProvider(ICourseRepository courseRepo, IUserRepository userRepo)
        {
            _courseRepo = courseRepo;
            _userRepo = userRepo;
        }

        public void CreateCourse(CourseDTO course)
        {
            var c = Mapper.Map<Course>(course);
            c.Teacher = _userRepo.Get(u => u.DomainUserId == course.Teacher.DomainUserId);
            _courseRepo.Add(c);
        }

        public CourseDTO GetByCourseCode(string courseCode)
        {
            var course = _courseRepo.Get(u => u.CourseCode == courseCode);
            return Mapper.Map<CourseDTO>(course);
        }

        public void UpdateCourse(CourseDTO course)
        {
            var cou = _courseRepo.Get(c=>c.CourseId == course.CourseId);
            cou.Teacher = _userRepo.Get(u => u.DomainUserId == course.Teacher.DomainUserId);
            _courseRepo.SaveChanges();
        }
    }
}
