using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Enums;
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

        public void SeedAll()
        {
            SeedRoles();
            SeedUsers();
            SeedCourses();
            SeedModules();
            SeedWikiPages();
            SeedFiles();
            SeedMesages();
            SeedHomeworks();
            SeedQuestions();
            SeedStudentQuestions();
            SeedAnswers();
            SeedTests();
        }

        private void SeedTests()
        {
            var module1 = _context.Modules.FirstOrDefault(x => x.ModuleId == 1);

            var question1 = _context.Questions.FirstOrDefault(x => x.QuestionId == 1);
            var question2 = _context.Questions.FirstOrDefault(x => x.QuestionId == 2);
            var question3 = _context.Questions.FirstOrDefault(x => x.QuestionId == 3);
            var question4 = _context.Questions.FirstOrDefault(x => x.QuestionId == 4);

            _context.Tests.AddOrUpdate(
                  new Test
                  {
                      Name = "Test1",
                      Module = module1,
                      Questions = new List<Question> { question1, question2 }
                  });

            _context.Tests.AddOrUpdate(
                  new Test
                  {
                      Name = "Test2",
                      Module = module1,
                      Questions = new List<Question> { question3, question4 }
                  });

        }

        private void SeedStudentQuestions()
        {
            var answer1 = _context.Answers.FirstOrDefault(x => x.AnswerId == 1);
            var answer2 = _context.Answers.FirstOrDefault(x => x.AnswerId == 2);
            var answer3 = _context.Answers.FirstOrDefault(x => x.AnswerId == 3);
            var answer4 = _context.Answers.FirstOrDefault(x => x.AnswerId == 4);


            var test1 = _context.Tests.FirstOrDefault(x => x.TestId == 1);
            var test2 = _context.Tests.FirstOrDefault(x => x.TestId == 2);

            _context.StudentQuestions.AddOrUpdate(
                  new StudentQuestion
                  {
                      DomainUserId = 1,
                      Answer = answer1,
                      QuestionId = 1,
                      Test = test1,
                  });

            _context.StudentQuestions.AddOrUpdate(
               new StudentQuestion
               {
                   DomainUserId = 1,
                   Answer = answer4,
                   QuestionId = 2,
                   Test = test1,
               });

            //------------------------------------


            _context.StudentQuestions.AddOrUpdate(
                  new StudentQuestion
                  {
                      DomainUserId = 2,
                      Answer = answer2,
                      QuestionId = 1,
                      Test = test1,
                  });

            _context.StudentQuestions.AddOrUpdate(
               new StudentQuestion
               {
                   DomainUserId = 2,
                   Answer = answer3,
                   QuestionId = 2,
                   Test = test1,
               });

            //------------------------------------

        }

        private void SeedAnswers()
        {
            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = true,
                      Text = "LL(1) "
                  });

            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = false,
                      Text = "Descendent Recursive "
                  });

            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = true,
                      Text = "Answer Correct "
                  });

            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = false,
                      Text = "Answer Incorrect "
                  });
        }

        private void SeedQuestions()
        {
            var module1 = _context.Modules.FirstOrDefault(x => x.ModuleId == 1);

            var answer1 = _context.Answers.FirstOrDefault(x => x.AnswerId == 1);
            var answer2 = _context.Answers.FirstOrDefault(x => x.AnswerId == 2);
            var answer3 = _context.Answers.FirstOrDefault(x => x.AnswerId == 3);
            var answer4 = _context.Answers.FirstOrDefault(x => x.AnswerId == 4);

            _context.Questions.AddOrUpdate(
                  new Question
                  {
                      Module = module1,
                      Text = "Which parser is mostly used? ",
                      Score = 100,
                      Answers = new List<Answer> { answer1, answer2 }
                  });

            _context.Questions.AddOrUpdate(
                  new Question
                  {
                      Module = module1,
                      Text = "Question1 ? ",
                      Score = 50,
                      Answers = new List<Answer> { answer3, answer4 }
                  });

            _context.Questions.AddOrUpdate(
                  new Question
                  {
                      Module = module1,
                      Text = "Question2 ? ",
                      Score = 70,
                      Answers = new List<Answer> { answer3, answer4 }
                  });

            _context.Questions.AddOrUpdate(
                  new Question
                  {
                      Module = module1,
                      Text = "Question3 ? ",
                      Score = 35,
                      Answers = new List<Answer> { answer3, answer4 }
                  });
        }

        private void SeedHomeworks()
        {
            var module1 = _context.Modules.FirstOrDefault(x => x.ModuleId == 1);
            var module2 = _context.Modules.FirstOrDefault(x => x.ModuleId == 2);

            _context.Homeworks.AddOrUpdate(
                  new Homework
                  {
                      Module = module1,
                      Text = "Homework 1",
                      Score = 100
                  });

            _context.Homeworks.AddOrUpdate(
                  new Homework
                  {
                      Module = module1,
                      Text = "Homework 2",
                      Score = 100
                  });

            _context.Homeworks.AddOrUpdate(
                  new Homework
                  {
                      Module = module2,
                      Text = "Homework 1",
                      Score = 100
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 1,
                      HomeworkId = 1,
                      Grade = 5
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 1,
                      HomeworkId = 2,
                      Grade = 7
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 1,
                      HomeworkId = 3,
                      Grade = 10
                  });
            //------------------------------------------------
            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 2,
                      HomeworkId = 1,
                      Grade = 10
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 2,
                      HomeworkId = 2,
                      Grade = 9
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 2,
                      HomeworkId = 3,
                      Grade = 8
                  });

        }

        private void SeedMesages()
        {
            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 1),
                      To = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 2),
                      Text = "Salut :)",
                      DateTime = DateTime.Now
                  });
            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 2),
                      To = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 1),
                      Text = "Hey :D",
                      DateTime = DateTime.Now
                  });
            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 1),
                      To = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 2),
                      Text = "Ce mai faci?",
                      DateTime = DateTime.Now
                  });
            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 2),
                      To = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 1),
                      Text = "Lucrez la proiect. Tu?",
                      DateTime = DateTime.Now
                  });

            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 1),
                      To = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 3),
                      Text = "Message 5",
                      DateTime = DateTime.Now
                  });
        }

        private void SeedFiles()
        {
            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File1.jpg",
                      FileType = FileType.jpg,
                      Path = "/student/iulia@euni.com/alabala.txt"
                  });

            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.pdf,
                      Path = "/admin/iulia@euni.com/alabala.txt"
                  });

            _context.Files.AddOrUpdate(
                  new File
                  {
                      StudentHomework = _context.StudentHomeworks.FirstOrDefault(x => x.DomainUserId == 1 && x.HomeworkId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.pdf,
                      Path = "path3"
                  });
        }

        private void SeedWikiPages()
        {
            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.FirstOrDefault(x => x.ModuleId == 1),
                      Content = "Content",
                      Description = "WikiPage 1"
                  });

            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.FirstOrDefault(x => x.ModuleId == 1),
                      Content = "Content",
                      Description = "WikiPage 2"
                  });

            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.FirstOrDefault(x => x.ModuleId == 1),
                      Content = "Content",
                      Description = "WikiPage 3"
                  });


            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.FirstOrDefault(x => x.ModuleId == 2),
                      Content = "Content",
                      Description = "WikiPage 4"
                  });

            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.FirstOrDefault(x => x.ModuleId == 2),
                      Content = "Content",
                      Description = "WikiPage 5"
                  });

        }

        private void SeedRoles()
        {

            var store = new RoleStore<IdentityRole>(_context);
            var manager = new RoleManager<IdentityRole>(store);
            var adminRole = new IdentityRole { Name = "admin" };
            var teacherRole = new IdentityRole { Name = "teacher" };
            var studentRole = new IdentityRole { Name = "student" };

            manager.Create(adminRole);
            manager.Create(teacherRole);
            manager.Create(studentRole);

        }

        private void SeedUsers()
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

        private void SeedCourses()
        {
            var user1 = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 1);
            var user2 = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 2);
            var user3 = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 3);
            var teacher = _context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 4);

            var course1 = new Course
            {
                CourseCode = "CD001",
                Name = "Compiler Design",
                StartDate = new DateTime(2015, 10, 01),
                EndDate = new DateTime(2016, 10, 01),
                Teacher = teacher,
                Students = new List<DomainUser> { user1, user2, user3 }
            };

            _context.Courses.AddOrUpdate(course1);

            _context.Courses.AddOrUpdate(
                new Course
                {
                    CourseCode = "WP001",
                    Name = "Web Programming",
                    StartDate = new DateTime(2015, 10, 01),
                    EndDate = new DateTime(2016, 10, 01),
                    Teacher = teacher,
                    Students = new List<DomainUser> { user1, user2, user3 }
                });

            _context.Courses.AddOrUpdate(
                new Course
                {
                    CourseCode = "PKC001",
                    Name = "Public Key Cryptography",
                    StartDate = new DateTime(2015, 10, 01),
                    EndDate = new DateTime(2016, 10, 01),
                    Teacher = teacher,
                    Students = new List<DomainUser> { user1, user2, user3 }
                });

            _context.SaveChanges();
        }

        private void SeedModules()
        {
            var course1 = _context.Courses.FirstOrDefault(x => x.CourseId == 1);
            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Module1",
                      Course = course1
                  }
                  );

            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Module2",
                      Course = course1
                  }
                  );

            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Module3",
                      Course = course1
                  }
                  );


            var course2 = _context.Courses.FirstOrDefault(x => x.CourseId == 2);
            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Module1",
                      Course = course2
                  }
                  );

            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Module2",
                      Course = course2
                  }
                  );

            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Module3",
                      Course = course2
                  }
                  );

            _context.SaveChanges();
        }
    }
}
