﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Domain;
using Microsoft.AspNet.Identity;

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
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("aaa");

            _context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "ana@euni.com",
                    PasswordHash = password,
                    DomainUser = new DomainUser
                    {
                        FirstName = "Ana",
                        LastName = "Pop",
                        MatriculationNumber = "AnPo01",
                        Email = "ana@euni.com",
                    }
                });

            _context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
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
                });

            _context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
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
                });

            _context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
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
                });

            _context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
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
                });

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
