namespace eUni.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Added_Many_To_Many_Tests_Questions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests");
            DropIndex("dbo.Questions", new[] { "Test_TestId" });
            CreateTable(
                "dbo.TestQuestions",
                c => new
                    {
                        Test_TestId = c.Int(nullable: false),
                        Question_QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Test_TestId, t.Question_QuestionId })
                .ForeignKey("dbo.Tests", t => t.Test_TestId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId, cascadeDelete: true)
                .Index(t => t.Test_TestId)
                .Index(t => t.Question_QuestionId);
            
            DropColumn("dbo.Questions", "Test_TestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Test_TestId", c => c.Int());
            DropForeignKey("dbo.TestQuestions", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.TestQuestions", "Test_TestId", "dbo.Tests");
            DropIndex("dbo.TestQuestions", new[] { "Question_QuestionId" });
            DropIndex("dbo.TestQuestions", new[] { "Test_TestId" });
            DropTable("dbo.TestQuestions");
            CreateIndex("dbo.Questions", "Test_TestId");
            AddForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests", "TestId");
        }
    }
}
