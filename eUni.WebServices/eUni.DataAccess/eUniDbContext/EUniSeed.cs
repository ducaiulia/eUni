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
            SeedAnswers();
            SeedModules();
            SeedWikiPages();
            SeedMesages();
            SeedFiles();
            SeedHomeworks();
            SeedQuestions();
            SeedTests();
            SeedStudentQuestions();
        }

        private void SeedTests()
        {
            var module1 = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1);

            var question1 = _context.Questions.FirstOrDefault(x => x.QuestionId == 1);
            var question2 = _context.Questions.FirstOrDefault(x => x.QuestionId == 2);
            var question3 = _context.Questions.FirstOrDefault(x => x.QuestionId == 3);
            var question4 = _context.Questions.FirstOrDefault(x => x.QuestionId == 4);

            _context.Tests.AddOrUpdate(
                  new Test
                  {
                      Name = "Compiler Introduction Test",
                      Module = module1,
                      Questions = new List<Question> { question1, question2 }
                  });

            _context.Tests.AddOrUpdate(
                  new Test
                  {
                      Name = "Compiler Parser Test",
                      Module = module1,
                      Questions = new List<Question> { question3, question4 }
                  });
            _context.SaveChanges();
        }

        private void SeedStudentQuestions()
        {
            var answer1 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 1);
            var answer2 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 2);
            var answer3 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 3);
            var answer4 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 4);


            var test1 = _context.Tests.Local.FirstOrDefault(x => x.TestId == 1);
            var test2 = _context.Tests.Local.FirstOrDefault(x => x.TestId == 2);

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
            _context.SaveChanges();
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
                      Text = "Descendent Recursive"
                  });

            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = true,
                      Text = "Yes"
                  });

            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = false,
                      Text = "No"
                  });

            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = false,
                      Text = "During syntax analysis"
                  });

            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = true,
                      Text = "	During syntax directed translation"
                  });
            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = false,
                      Text = "12 ms"
                  });

            _context.Answers.AddOrUpdate(
                  new Answer
                  {
                      IsCorrect = true,
                      Text = "0.32 ms"
                  });

            _context.SaveChanges();
        }

        private void SeedQuestions()
        {
            var module1 = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1);

            var answer1 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 1);
            var answer2 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 2);
            var answer3 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 3);
            var answer4 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 4);
            var answer5 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 5);
            var answer6 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 6);
            var answer7 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 7);
            var answer8 = _context.Answers.Local.FirstOrDefault(x => x.AnswerId == 8);

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
                      Text = "What is the average running time of a parsing algorithm?",
                      Score = 50,
                      Answers = new List<Answer> { answer7, answer8 }
                  });

            _context.Questions.AddOrUpdate(
                  new Question
                  {
                      Module = module1,
                      Text = "Are compilers important? ",
                      Score = 70,
                      Answers = new List<Answer> { answer3, answer4 }
                  });

            _context.Questions.AddOrUpdate(
                  new Question
                  {
                      Module = module1,
                      Text = "When is the type checking usually done? ? ",
                      Score = 35,
                      Answers = new List<Answer> { answer5, answer6 }
                  });
            _context.SaveChanges();

        }

        private void SeedHomeworks()
        {
            var module1 = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1);
            var module2 = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 2);

            _context.Homeworks.AddOrUpdate(
                  new Homework
                  {
                      Module = module1,
                      Text = "Parsing Algorithms ",
                      Score = 100
                  });

            _context.Homeworks.AddOrUpdate(
                  new Homework
                  {
                      Module = module1,
                      Text = "Lexical Analysis",
                      Score = 100
                  });

            _context.Homeworks.AddOrUpdate(
                  new Homework
                  {
                      Module = module2,
                      Text = "High Level Programming",
                      Score = 100
                  });
            //------------------------------------------------
            var file3 = _context.Files.Local.SingleOrDefault(x => x.Id == 3);
            var file4 = _context.Files.Local.SingleOrDefault(x => x.Id == 4);
            var file5 = _context.Files.Local.SingleOrDefault(x => x.Id == 5);
            var file6 = _context.Files.Local.SingleOrDefault(x => x.Id == 6);
            var file7 = _context.Files.Local.SingleOrDefault(x => x.Id == 7);
            var file8 = _context.Files.Local.SingleOrDefault(x => x.Id == 8);
            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 1,
                      HomeworkId = 1,
                      Grade = 5,
                      Files = new List<File> { file3, file4 }
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 1,
                      HomeworkId = 2,
                      Grade = 7,
                      Files = new List<File> { file5 }
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 1,
                      HomeworkId = 3,
                      Grade = 10,
                      Files = new List<File> { file6 }
                  });
            //------------------------------------------------
            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 2,
                      HomeworkId = 1,
                      Grade = 10,
                      Files = new List<File> { file7 }
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 2,
                      HomeworkId = 2,
                      Grade = 9,
                      Files = new List<File> { file8 }
                  });

            _context.StudentHomeworks.AddOrUpdate(
                  new StudentHomework
                  {
                      DomainUserId = 2,
                      HomeworkId = 3,
                      Grade = 8,
                      //Files = new List<File> { file4 }
                  });

            _context.SaveChanges();
        }

        private void SeedMesages()
        {
            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 1),
                      To = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 2),
                      Text = "Salut :)",
                      DateTime = DateTime.Now
                  });
            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 2),
                      To = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 1),
                      Text = "Hey :D",
                      DateTime = DateTime.Now
                  });
            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 1),
                      To = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 2),
                      Text = "Ce mai faci?",
                      DateTime = DateTime.Now
                  });
            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 2),
                      To = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 1),
                      Text = "Lucrez la proiect. Tu?",
                      DateTime = DateTime.Now
                  });

            _context.Messages.AddOrUpdate(
                  new Message
                  {
                      From = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 1),
                      To = _context.DomainUsers.Local.FirstOrDefault(x => x.DomainUserId == 3),
                      Text = "Hello.",
                      DateTime = DateTime.Now
                  });
            _context.SaveChanges();

        }

        private void SeedFiles()
        {
            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File1.jpg",
                      FileType = FileType.jpg,
                      Path = "/student/iulia@euni.com/alabala.txt"
                  });

            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.pdf,
                      Path = "/admin/iulia@euni.com/alabala.txt"
                  });
            _context.SaveChanges();

            //for students homeworks
            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.txt,
                      Path = "/admin/iulia@euni.com/alabala.txt"
                  });
            _context.SaveChanges();

            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.txt,
                      Path = "/admin/iulia@euni.com/alabala.txt"
                  });
            _context.SaveChanges();

            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.txt,
                      Path = "/admin/iulia@euni.com/alabala.txt"
                  });
            _context.SaveChanges();

            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.txt,
                      Path = "/admin/iulia@euni.com/alabala.txt"
                  });
            _context.SaveChanges();

            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.txt,
                      Path = "/admin/iulia@euni.com/alabala.txt"
                  });
            _context.SaveChanges();

            _context.Files.AddOrUpdate(
                  new File
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      FileName = "File2.pdf",
                      FileType = FileType.txt,
                      Path = "/admin/iulia@euni.com/alabala.txt"
                  });
            _context.SaveChanges();
        }

        private void SeedWikiPages()
        {
            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      Content = "<h2>Compiler Design Introduction</h2><p>This course covers the design and implementation of compiler and runtime systems for high-level languages, and examines the interaction between language design, compiler design, and runtime organization. Topics covered include lexical and syntactic analysis, handling of user-defined types and type-checking, context analysis, code generation and optimization, and memory management and runtime organization.</p>",
                      Description = "Compiler Design Introduction"
                  });

            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      Content = "<h1>Parsing Algorithms</h1><p>In computer science, an LALR parser[a] or Look-Ahead LR parser is a simplified version of a canonical LR parser, to parse (separate and analyze) a text according to a set of production rules specified by a formal grammar for a computer language. (LR means left-to-right, rightmost derivation.)",
                      Description = "Parsing Algorithms"
                  });

            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 1),
                      Content = "<h2>Lexical Analysis<h2><p>Lexical analysis is the first phase of a compiler. It takes the modified source code from language preprocessors that are written in the form of sentences. The lexical analyzer breaks these syntaxes into a series of tokens, by removing any whitespace or comments in the source code.</p> <p>If the lexical analyzer finds a token invalid, it generates an error. The lexical analyzer works closely with the syntax analyzer. It reads character streams from the source code, checks for legal tokens, and passes the data to the syntax analyzer when it demands.</p>",
                      Description = "Lexical Analysis"
                  });


            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 2),
                      Content = "<h1>High Level Programming</h1> <p>Lexemes are said to be a sequence of characters (alphanumeric) in a token. There are some predefined rules for every lexeme to be identified as a valid token. These rules are defined by grammar rules, by means of a pattern. A pattern explains what can be a token, and these patterns are defined by means of regular expressions.</p>",
                      Description = "High Level Programming"
                  });

            _context.WikiPages.AddOrUpdate(
                  new WikiPage
                  {
                      Module = _context.Modules.Local.FirstOrDefault(x => x.ModuleId == 2),
                      Content = "<h1>High Level Algorithms</h1> <p>While scanning both lexemes till ‘int’, the lexical analyzer cannot determine whether it is a keyword int or the initials of identifier int value.</p> <p>The Longest Match Rule states that the lexeme scanned should be determined based on the longest match among all the tokens available.</p>",
                      Description = "High Level Algorithms"
                  });
            _context.SaveChanges();

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
            _context.SaveChanges();

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

            var user7 = new ApplicationUser
            {
                UserName = "adrian@euni.com",
                PasswordHash = password,
                DomainUser = new DomainUser
                {
                    FirstName = "Adrian",
                    LastName = "Achim",
                    MatriculationNumber = "AdAc01",
                    Email = "adrian@euni.com",
                }
            };

            manager.Create(user7);
            manager.AddToRole(user7.Id, "Teacher");

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
                      Name = "Compiler Design Introductory Skills",
                      Course = course1
                  });

            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Compiler Design Intermediate Exercices",
                      Course = course1
                  });

            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Compiler Design Advanced Algorithms",
                      Course = course1
                  });


            var course2 = _context.Courses.FirstOrDefault(x => x.CourseId == 2);
            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Html Easy",
                      Course = course2
                  });

            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Java Script",
                      Course = course2
                  });

            _context.Modules.AddOrUpdate(
                  new Module
                  {
                      Name = "Angular JS",
                      Course = course2
                  });

            _context.SaveChanges();
        }
    }
}
