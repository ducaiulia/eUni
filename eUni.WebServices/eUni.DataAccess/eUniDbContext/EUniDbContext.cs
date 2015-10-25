using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eUni.DataAccess.Domain;

namespace eUni.DataAccess.eUniDbContext
{
    public class EUniDbContext : DbContext
    {
        public EUniDbContext()
            : base("EUniDatabase")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EUniDbContext>());
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<StudentQuestion> StudentQuestions { get; set; }
        public DbSet<Content> Contents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new DateTime2Convention());
        }
    }
}
