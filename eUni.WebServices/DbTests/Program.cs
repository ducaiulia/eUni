using System;
using System.Data.Entity.Migrations;
using System.Linq;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;

namespace DbTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            //var seed = new EUniSeed(context);
            //seed.SeedAll();


            var shw = context.StudentHomeworks.FirstOrDefault();

            Console.WriteLine(shw.DomainUser.FirstName+" "+shw.Homework.Text);

            Console.ReadLine();
        }
    }
}

