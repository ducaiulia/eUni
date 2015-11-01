namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .Index(t => t.Question_QuestionId);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Module_ModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.Module_ModuleId)
                .Index(t => t.Module_ModuleId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CourseCode = c.String(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Teacher_DomainUserId = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.DomainUsers", t => t.Teacher_DomainUserId)
                .Index(t => t.Teacher_DomainUserId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Course_CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.ModuleId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.DomainUsers",
                c => new
                    {
                        DomainUserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        MatriculationNumber = c.String(),
                    })
                .PrimaryKey(t => t.DomainUserId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Score = c.Int(nullable: false),
                        Module_ModuleId = c.Int(),
                        Test_TestId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Modules", t => t.Module_ModuleId)
                .ForeignKey("dbo.Tests", t => t.Test_TestId)
                .Index(t => t.Module_ModuleId)
                .Index(t => t.Test_TestId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.StudentQuestions",
                c => new
                    {
                        DomainUserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Answer_AnswerId = c.Int(),
                        Test_TestId = c.Int(),
                    })
                .PrimaryKey(t => new { t.DomainUserId, t.QuestionId })
                .ForeignKey("dbo.Answers", t => t.Answer_AnswerId)
                .ForeignKey("dbo.DomainUsers", t => t.DomainUserId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.Test_TestId)
                .Index(t => t.DomainUserId)
                .Index(t => t.QuestionId)
                .Index(t => t.Answer_AnswerId)
                .Index(t => t.Test_TestId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Module_ModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.Modules", t => t.Module_ModuleId)
                .Index(t => t.Module_ModuleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StudentTests",
                c => new
                    {
                        DomainUserId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DomainUserId, t.TestId })
                .ForeignKey("dbo.DomainUsers", t => t.DomainUserId, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.DomainUserId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.StudentHomeworks",
                c => new
                    {
                        DomainUserId = c.Int(nullable: false),
                        HomeworkId = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DomainUserId, t.HomeworkId })
                .ForeignKey("dbo.DomainUsers", t => t.DomainUserId, cascadeDelete: true)
                .ForeignKey("dbo.Homework", t => t.HomeworkId, cascadeDelete: true)
                .Index(t => t.DomainUserId)
                .Index(t => t.HomeworkId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Path = c.String(),
                        Size = c.Int(nullable: false),
                        FileType = c.Int(nullable: false),
                        StudentHomework_DomainUserId = c.Int(),
                        StudentHomework_HomeworkId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentHomeworks", t => new { t.StudentHomework_DomainUserId, t.StudentHomework_HomeworkId })
                .Index(t => new { t.StudentHomework_DomainUserId, t.StudentHomework_HomeworkId });
            
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        HomeworkId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Score = c.Int(nullable: false),
                        Module_ModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.HomeworkId)
                .ForeignKey("dbo.Modules", t => t.Module_ModuleId)
                .Index(t => t.Module_ModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentHomeworks", "HomeworkId", "dbo.Homework");
            DropForeignKey("dbo.Homework", "Module_ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Files", new[] { "StudentHomework_DomainUserId", "StudentHomework_HomeworkId" }, "dbo.StudentHomeworks");
            DropForeignKey("dbo.StudentHomeworks", "DomainUserId", "dbo.DomainUsers");
            DropForeignKey("dbo.StudentTests", "TestId", "dbo.Tests");
            DropForeignKey("dbo.StudentTests", "DomainUserId", "dbo.DomainUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentQuestions", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Module_ModuleId", "dbo.Modules");
            DropForeignKey("dbo.StudentQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.StudentQuestions", "DomainUserId", "dbo.DomainUsers");
            DropForeignKey("dbo.StudentQuestions", "Answer_AnswerId", "dbo.Answers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Questions", "Module_ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Answers", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Courses", "Teacher_DomainUserId", "dbo.DomainUsers");
            DropForeignKey("dbo.Modules", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Contents", "Module_ModuleId", "dbo.Modules");
            DropIndex("dbo.Homework", new[] { "Module_ModuleId" });
            DropIndex("dbo.Files", new[] { "StudentHomework_DomainUserId", "StudentHomework_HomeworkId" });
            DropIndex("dbo.StudentHomeworks", new[] { "HomeworkId" });
            DropIndex("dbo.StudentHomeworks", new[] { "DomainUserId" });
            DropIndex("dbo.StudentTests", new[] { "TestId" });
            DropIndex("dbo.StudentTests", new[] { "DomainUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Tests", new[] { "Module_ModuleId" });
            DropIndex("dbo.StudentQuestions", new[] { "Test_TestId" });
            DropIndex("dbo.StudentQuestions", new[] { "Answer_AnswerId" });
            DropIndex("dbo.StudentQuestions", new[] { "QuestionId" });
            DropIndex("dbo.StudentQuestions", new[] { "DomainUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Questions", new[] { "Test_TestId" });
            DropIndex("dbo.Questions", new[] { "Module_ModuleId" });
            DropIndex("dbo.Modules", new[] { "Course_CourseId" });
            DropIndex("dbo.Courses", new[] { "Teacher_DomainUserId" });
            DropIndex("dbo.Contents", new[] { "Module_ModuleId" });
            DropIndex("dbo.Answers", new[] { "Question_QuestionId" });
            DropTable("dbo.Homework");
            DropTable("dbo.Files");
            DropTable("dbo.StudentHomeworks");
            DropTable("dbo.StudentTests");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tests");
            DropTable("dbo.StudentQuestions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Questions");
            DropTable("dbo.DomainUsers");
            DropTable("dbo.Modules");
            DropTable("dbo.Courses");
            DropTable("dbo.Contents");
            DropTable("dbo.Answers");
        }
    }
}
