﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eUni.BusinessLogic.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepo;
        private readonly IAspNetUserRepository _aspNetUserRepo;
        private readonly ICourseRepository _courseRepo;
        public UserProvider(IUserRepository userRepo, IAspNetUserRepository aspNetUserRepo, ICourseRepository courseRepo)
        {
            _userRepo = userRepo;
            _aspNetUserRepo = aspNetUserRepo;
            _courseRepo = courseRepo;
        }

        public List<DomainUserDTO> GetAllUsers()
        {
            IEnumerable<DomainUser> enumerableAllUsers = _userRepo.GetAll();
            var allUsers = Mapper.Map<IEnumerable<DomainUserDTO>>(enumerableAllUsers);

            return allUsers.ToList();
        }
        public DomainUserDTO GetByUserName(string userName)
        {
            var user = _aspNetUserRepo.Get(u => u.UserName.Trim().Equals(userName));
            return Mapper.Map<DomainUserDTO>(user.DomainUser);
        }

        public DomainUserDTO GetByName(string lastName,string firstName)
        {
            var user = _userRepo.Get(u => u.FirstName.Trim().Equals(firstName) && u.LastName.Trim().Equals(lastName));
            return Mapper.Map<DomainUserDTO>(user);
        }

        /// <summary>
        /// Enroll the user to a course.
        /// </summary>
        /// <param name="courseCode">the course code to match the course.</param>
        /// <returns></returns>
        public ResultActionDTO EnrollUserToCourse(string courseCode, int domainUserId)
        {
            var course = _courseRepo.Get(u => u.CourseCode == courseCode);
            var user = _userRepo.Get(x => x.DomainUserId == domainUserId);
            if (course == null)
            {
                return new ResultActionDTO() { Succeeded = false, ErrorMessage = "Course not found!" };
            }
            if (user == null)
            {
                return new ResultActionDTO() { Succeeded = false, ErrorMessage = "User not found!" };
            }
            course.Students.Add(user);
            _courseRepo.SaveChanges();

            return new ResultActionDTO()
            {
                Succeeded = true,
                ErrorMessage = "User enrolled to course"
            };

        }

        public List<DomainUserDTO> GetAllStudents()
        {
            ICollection<IdentityUserRole> users;
            using (var db = new ApplicationDbContext())
            {
                var role = db.Roles.AsQueryable().FirstOrDefault(r => r.Name.Equals("student"));
                users = role.Users;
            }
            List<DomainUserDTO> studentUsers = new List<DomainUserDTO>();
            foreach (var user in users)
            {
                studentUsers.Add(Mapper.Map<DomainUserDTO>(_userRepo.Get(u => u.ApplicationUser.Id == user.UserId)));
            }

            return studentUsers;
        }

        public List<DomainUserDTO> GetAllTeachers()
        {
            ICollection<IdentityUserRole> users;
            using (var db = new ApplicationDbContext())
            {
                var role = db.Roles.AsQueryable().FirstOrDefault(r => r.Name.Equals("teacher"));
                users = role.Users;
            }
            List<DomainUserDTO> teacherUsers = new List<DomainUserDTO>();
            foreach (var user in users)
            {
                teacherUsers.Add(Mapper.Map<DomainUserDTO>(_userRepo.Get(u => u.ApplicationUser.Id == user.UserId)));
            }

            return teacherUsers;
        }
    }
}
