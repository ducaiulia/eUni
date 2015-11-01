using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Domain;

namespace eUni.DataAccess.eUniDbContext
{
    public class EUniSeed
    {
        private readonly ApplicationDbContext _context;
        public EUniSeed(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            var user1 = new DomainUser
            {
                FirstName = "Adela",
                LastName = "Berindean",
                MatriculationNumber = "adbe01",
                //Username = "adela.berindean",
                Email = "adela@yahoo.com",
                //Password = "aaa",
            };
            _context.DomainUsers.AddOrUpdate(user1);

            var user2 = new DomainUser
            {
                FirstName = "Iuliana",
                LastName = "Duca",
                MatriculationNumber = "iudu02",
                //Username = "iulia.duca",
                Email = "iulia@yahoo.com",
                //Password = "aaa",
            };
            _context.DomainUsers.AddOrUpdate(user2);

            var user3 = new DomainUser
            {
                FirstName = "Adina",
                LastName = "Duma",
                MatriculationNumber = "addu03",
                //Username = "adina.duma",
                Email = "adina@yahoo.com",
                //Password = "aaa",
            };
            _context.DomainUsers.AddOrUpdate(user3);

        }

        public void SeedCourses()
        {
            var course1 = new Course
            {
                CourseCode = "CD001",
                Name = "Compiler Design",
                StartDate = new DateTime(2015,10,01)
            };
        }
    }
}
