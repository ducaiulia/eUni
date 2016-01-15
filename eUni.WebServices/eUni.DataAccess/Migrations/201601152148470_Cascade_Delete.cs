namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade_Delete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DomainUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            AddForeignKey("dbo.DomainUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DomainUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            AddForeignKey("dbo.DomainUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
