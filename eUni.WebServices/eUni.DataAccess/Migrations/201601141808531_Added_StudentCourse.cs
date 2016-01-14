namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_StudentCourse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DomainUsers", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.DomainUsers", new[] { "Course_CourseId" });
            CreateTable(
                "dbo.CourseDomainUsers",
                c => new
                    {
                        Course_CourseId = c.Int(nullable: false),
                        DomainUser_DomainUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_CourseId, t.DomainUser_DomainUserId })
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .ForeignKey("dbo.DomainUsers", t => t.DomainUser_DomainUserId, cascadeDelete: true)
                .Index(t => t.Course_CourseId)
                .Index(t => t.DomainUser_DomainUserId);
            
            DropColumn("dbo.DomainUsers", "Course_CourseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DomainUsers", "Course_CourseId", c => c.Int());
            DropForeignKey("dbo.CourseDomainUsers", "DomainUser_DomainUserId", "dbo.DomainUsers");
            DropForeignKey("dbo.CourseDomainUsers", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.CourseDomainUsers", new[] { "DomainUser_DomainUserId" });
            DropIndex("dbo.CourseDomainUsers", new[] { "Course_CourseId" });
            DropTable("dbo.CourseDomainUsers");
            CreateIndex("dbo.DomainUsers", "Course_CourseId");
            AddForeignKey("dbo.DomainUsers", "Course_CourseId", "dbo.Courses", "CourseId");
        }
    }
}
