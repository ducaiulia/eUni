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

            var user1 = context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 1);
            var user2 = context.DomainUsers.FirstOrDefault(x => x.DomainUserId == 2);

            context.Messages.AddOrUpdate(new Message
            {
                From = user1,
                To = user2,
                DateTime = DateTime.Now,
                Text = "wedfwefw"
            });
            context.SaveChanges();
            var m = context.Messages.FirstOrDefault();
            Console.WriteLine(m.Text + m.From.DomainUserId + m.To.DomainUserId + m.DateTime);

            Console.ReadLine();
        }
    }
}

