namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        Teacher_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Users", t => t.Teacher_UserId)
                .Index(t => t.Teacher_UserId);
            
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
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        MatriculationNumber = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
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
                "dbo.StudentQuestions",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Answer_AnswerId = c.Int(),
                        Test_TestId = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.QuestionId })
                .ForeignKey("dbo.Answers", t => t.Answer_AnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.Test_TestId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
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
                "dbo.StudentTests",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TestId })
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.StudentHomeworks",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        HomeworkId = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.HomeworkId })
                .ForeignKey("dbo.Homework", t => t.HomeworkId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
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
                        StudentHomework_UserId = c.Int(),
                        StudentHomework_HomeworkId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentHomeworks", t => new { t.StudentHomework_UserId, t.StudentHomework_HomeworkId })
                .Index(t => new { t.StudentHomework_UserId, t.StudentHomework_HomeworkId });
            
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
            DropForeignKey("dbo.StudentHomeworks", "UserId", "dbo.Users");
            DropForeignKey("dbo.StudentHomeworks", "HomeworkId", "dbo.Homework");
            DropForeignKey("dbo.Homework", "Module_ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Files", new[] { "StudentHomework_UserId", "StudentHomework_HomeworkId" }, "dbo.StudentHomeworks");
            DropForeignKey("dbo.StudentTests", "UserId", "dbo.Users");
            DropForeignKey("dbo.StudentTests", "TestId", "dbo.Tests");
            DropForeignKey("dbo.StudentQuestions", "UserId", "dbo.Users");
            DropForeignKey("dbo.StudentQuestions", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Module_ModuleId", "dbo.Modules");
            DropForeignKey("dbo.StudentQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.StudentQuestions", "Answer_AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Questions", "Module_ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Answers", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Courses", "Teacher_UserId", "dbo.Users");
            DropForeignKey("dbo.Modules", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Contents", "Module_ModuleId", "dbo.Modules");
            DropIndex("dbo.Homework", new[] { "Module_ModuleId" });
            DropIndex("dbo.Files", new[] { "StudentHomework_UserId", "StudentHomework_HomeworkId" });
            DropIndex("dbo.StudentHomeworks", new[] { "HomeworkId" });
            DropIndex("dbo.StudentHomeworks", new[] { "UserId" });
            DropIndex("dbo.StudentTests", new[] { "TestId" });
            DropIndex("dbo.StudentTests", new[] { "UserId" });
            DropIndex("dbo.Tests", new[] { "Module_ModuleId" });
            DropIndex("dbo.StudentQuestions", new[] { "Test_TestId" });
            DropIndex("dbo.StudentQuestions", new[] { "Answer_AnswerId" });
            DropIndex("dbo.StudentQuestions", new[] { "QuestionId" });
            DropIndex("dbo.StudentQuestions", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "Test_TestId" });
            DropIndex("dbo.Questions", new[] { "Module_ModuleId" });
            DropIndex("dbo.Modules", new[] { "Course_CourseId" });
            DropIndex("dbo.Courses", new[] { "Teacher_UserId" });
            DropIndex("dbo.Contents", new[] { "Module_ModuleId" });
            DropIndex("dbo.Answers", new[] { "Question_QuestionId" });
            DropTable("dbo.Homework");
            DropTable("dbo.Files");
            DropTable("dbo.StudentHomeworks");
            DropTable("dbo.StudentTests");
            DropTable("dbo.Tests");
            DropTable("dbo.StudentQuestions");
            DropTable("dbo.Questions");
            DropTable("dbo.Users");
            DropTable("dbo.Modules");
            DropTable("dbo.Courses");
            DropTable("dbo.Contents");
            DropTable("dbo.Answers");
        }
    }
}
