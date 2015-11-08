using AutoMapper;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.Providers
{
    public class CourseProvider:AbstractProvider
    {
        private readonly ICourseRepository _courseRepo = new CourseRepository(context);
        private readonly IUserRepository _userRepo = new UserRepository(context);

        public void CreateCourse(CourseDTO course)
        {
            var c = Mapper.Map<Course>(course);
            c.Teacher = _userRepo.Get(u=>u.DomainUserId==course.Teacher.DomainUserId);
            _courseRepo.Add(c);
        }
    }
}
