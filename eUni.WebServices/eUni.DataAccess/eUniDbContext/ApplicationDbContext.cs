using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUni.DataAccess.Domain;
using eUni.DataAccess.Repository;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eUni.DataAccess.eUniDbContext
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("EUniDatabase", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Log> Logs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<DomainUser> DomainUsers { get; set; }
        public DbSet<IdentityRole> IdentityRoles { get; set; }
        public DbSet<IdentityUserRole> IdentityUserRoles { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<StudentQuestion> StudentQuestions { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<WikiPage> WikiPages { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(new DateTime2Convention());

            // modelBuilder.Entity<DomainUser>().HasKey(q => q.UserId);
            //modelBuilder.Entity<Test>().HasKey(q => q.TestId);
            modelBuilder.Entity<StudentTest>().HasKey(q =>
                new {
                    q.DomainUserId,
                    q.TestId
                });
            modelBuilder.Entity<StudentQuestion>().HasKey(q =>
                new {
                    q.DomainUserId,
                    q.QuestionId
                });
            modelBuilder.Entity<StudentHomework>().HasKey(q =>
                new {
                    q.DomainUserId,
                    q.HomeworkId
                });

            // Relationships
            modelBuilder.Entity<ApplicationUser>().HasOptional(au => au.DomainUser).WithRequired(x => x.ApplicationUser);

            modelBuilder.Entity<StudentTest>()
                .HasRequired(t => t.Test)
                .WithMany()
                .HasForeignKey(t => t.TestId);

            modelBuilder.Entity<StudentTest>()
                .HasRequired(t => t.DomainUser)
                .WithMany()
                .HasForeignKey(t => t.DomainUserId);

            modelBuilder.Entity<Course>()
                .HasMany(x => x.Students)
                .WithOptional();
        }
    }
}
