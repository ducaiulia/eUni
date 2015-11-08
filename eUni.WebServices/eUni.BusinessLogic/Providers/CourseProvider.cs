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
    }
}
