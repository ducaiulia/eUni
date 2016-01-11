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
        List<DomainUserDTO> GetAllUsers(PaginationFilter filter);
        DomainUserDTO GetByName(string lastName, string firstName);
        ResultActionDTO EnrollUserToCourse(string courseCode, int domainUserId);
        List<DomainUserDTO> GetAllStudentsWithPagination(PaginationFilter filter);
        List<DomainUserDTO> GetAllStudents();
        List<DomainUserDTO> GetAllTeachers();
        List<DomainUserDTO> GetAllAdmins();
    }
}
