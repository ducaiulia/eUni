﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.BusinessLogic.Providers.DataTransferObjects;

namespace eUni.BusinessLogic.IProviders
{
    public interface IUserProvider
    {
        DomainUserDTO GetByUserName(string getFromToken);
        List<DomainUserDTO> GetAllUsers();
        List<DomainUserDTO> GetAllUsersWithPagination(PaginationFilter filter);
        DomainUserDTO GetByName(string lastName, string firstName);
        ResultActionDTO EnrollUserToCourse(int courseId, int userId);
        List<DomainUserDTO> GetAllStudentsWithPagination(PaginationFilter filter);
        List<DomainUserDTO> GetAllStudents();
        List<DomainUserDTO> GetAllTeachers();
        List<DomainUserDTO> GetAllTeachersWithPagination(PaginationFilter filter);
        List<DomainUserDTO> GetAllAdmins();
        List<DomainUserDTO> GetAllAdminsWithPagination(PaginationFilter filter);
        List<DomainUserDTO> GetAllStudentsByCourseId(int courseId);
        List<DomainUserDTO> GetAllStudentsByCourseIdPagination(int courseId, PaginationFilter filter);
    }
}
