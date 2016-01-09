using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eUni.DataAccess.eUniDbContext
{
    public class EUniSeed
    {
        private readonly ApplicationDbContext _context;
        public EUniSeed(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedRoles()
        {

            var store = new RoleStore<IdentityRole>(_context);
            var manager = new RoleManager<IdentityRole>(store);
            var adminRole = new IdentityRole { Name = "Admin" };
            var teacherRole = new IdentityRole { Name = "Teacher" };
            var studentRole = new IdentityRole { Name = "Student" };

            manager.Create(adminRole);
            manager.Create(teacherRole);
            manager.Create(studentRole);
            
        }

        public void SeedUsers()
        {
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("aaa");

            var store = new UserStore<ApplicationUser>(_context);
            var manager = new UserManager<ApplicationUser>(store);


            var user1 = new ApplicationUser
            {
                UserName = "ana@euni.com",
                PasswordHash = password,
                DomainUser = new DomainUser
                {
                    FirstName = "Ana",
                    LastName = "Pop",
                    MatriculationNumber = "AnPo01",
                    Email = "ana@euni.com"
                }
            };

            manager.Create(user1);
            manager.AddToRole(user1.Id, "Student");

            var user2 = new ApplicationUser
            {
                UserName = "marius@euni.com",
                PasswordHash = password,
                DomainUser = new DomainUser
                {
                    FirstName = "Marius",
                    LastName = "Muresan",
                    MatriculationNumber = "MaMu01",
                    Email = "marius@euni.com",
                }
            };
            manager.Create(user2);
            manager.AddToRole(user2.Id, "Student");


            var user3 = new ApplicationUser
            {
                UserName = "adela@euni.com",
                PasswordHash = password,
                DomainUser = new DomainUser
                {
                    FirstName = "Adela",
                    LastName = "Berindean",
                    MatriculationNumber = "AdBe01",
                    Email = "adela@euni.com",
                }

            };
            manager.Create(user3);
            manager.AddToRole(user3.Id, "Student");


            var user4 = new ApplicationUser
            {
                UserName = "adina@euni.com",
                PasswordHash = password,
                DomainUser = new DomainUser
                {
                    FirstName = "Adina",
                    LastName = "Duma",
                    MatriculationNumber = "AdDu01",
                    Email = "adina@euni.com",
                }
            };

            manager.Create(user4);
            manager.AddToRole(user4.Id, "Teacher");

            var user5 = new ApplicationUser
            {
                UserName = "iulia@euni.com",
                PasswordHash = password,
                DomainUser = new DomainUser
                {
                    FirstName = "Iulia",
                    LastName = "Duca",
                    MatriculationNumber = "IuDu01",
                    Email = "iulia@euni.com",
                }
            };

            manager.Create(user5);
            manager.AddToRole(user5.Id, "Admin");


            _context.SaveChanges();

        }

        public void SeedCourses()
        {
            _context.Courses.AddOrUpdate(
                new Course
                {
                    CourseCode = "CD001",
                    Name = "Compiler Design",
                    StartDate = new DateTime(2015, 10, 01),
                    EndDate = new DateTime(2016, 10, 01),
                    Teacher = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 1)
                }
                );

            _context.Courses.AddOrUpdate(
                new Course
                {
                    CourseCode = "WP001",
                    Name = "Web Programming",
                    StartDate = new DateTime(2015, 10, 01),
                    EndDate = new DateTime(2016, 10, 01),
                    Teacher = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 2)
                }
                );
            _context.Courses.AddOrUpdate(
                new Course
                {
                    CourseCode = "PKC001",
                    Name = "Public Key Cryptography",
                    StartDate = new DateTime(2015, 10, 01),
                    EndDate = new DateTime(2016, 10, 01),
                    Teacher = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 3)
                }
                );
            _context.SaveChanges();
        }
    }
}
