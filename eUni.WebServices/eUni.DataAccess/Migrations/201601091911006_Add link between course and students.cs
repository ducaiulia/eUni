namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addlinkbetweencourseandstudents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DomainUsers", "Course_CourseId", c => c.Int());
            CreateIndex("dbo.DomainUsers", "Course_CourseId");
            AddForeignKey("dbo.DomainUsers", "Course_CourseId", "dbo.Courses", "CourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DomainUsers", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.DomainUsers", new[] { "Course_CourseId" });
            DropColumn("dbo.DomainUsers", "Course_CourseId");
        }
    }
}
