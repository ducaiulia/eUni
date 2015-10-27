using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Domain;
using eUni.DataAccess.eUniDbContext;

namespace DbTests
{
    class Program
    {
        static void Main(string[] args)
        {
            EUniDbContext context = new EUniDbContext();
            context.Courses.Add(new Course() {CourseId = 1});
            context.SaveChanges();
            Console.ReadLine();
        }
    }
}
