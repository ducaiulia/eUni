using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;

namespace DbTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            foreach (var user in context.Users.ToList())
            {
                Console.WriteLine(user.UserName);
                Console.WriteLine(user.DomainUser.FirstName);
            }
            UserRepository ur = new UserRepository(context);
            
            DomainUser d = ur.Get(x => x.DomainUserId == 1);

            context.Courses.AddOrUpdate(new Course
            {
                Name = "c1",
                Teacher = d
            });
            context.SaveChanges();



            //    _context.SaveChanges();

            //    var a = _context.Users.FirstOrDefault();
            //    Console.WriteLine(a.DomainUser.FirstName);
            //    Console.ReadLine();
            //}
            ApplicationDbContext context = new ApplicationDbContext();
            foreach (var user in context.Users.ToList())
            {
                Console.WriteLine(user.UserName);
                Console.WriteLine(user.DomainUser.FirstName);
            }
            UserRepository ur = new UserRepository(context);

            DomainUser d = ur.Get(x => x.DomainUserId == 1);

            context.Courses.AddOrUpdate(new Course
            {
                Name = "c1",
                Teacher = d
            });
            context.SaveChanges();




            Console.ReadLine();
        }
    }
}
   
