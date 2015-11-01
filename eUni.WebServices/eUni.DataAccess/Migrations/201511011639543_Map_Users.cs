namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Map_Users : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DomainUsers", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.DomainUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.DomainUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DomainUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DomainUsers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.DomainUsers", "ApplicationUser_Id");
        }
    }
}
