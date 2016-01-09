namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddconnectionbetweenFileandModule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Module_ModuleId", c => c.Int());
            CreateIndex("dbo.Files", "Module_ModuleId");
            AddForeignKey("dbo.Files", "Module_ModuleId", "dbo.Modules", "ModuleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "Module_ModuleId", "dbo.Modules");
            DropIndex("dbo.Files", new[] { "Module_ModuleId" });
            DropColumn("dbo.Files", "Module_ModuleId");
        }
    }
}
