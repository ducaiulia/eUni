namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_WikiPages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WikiPages",
                c => new
                    {
                        WikiPageId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Content = c.String(unicode: false),
                        Module_ModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.WikiPageId)
                .ForeignKey("dbo.Modules", t => t.Module_ModuleId)
                .Index(t => t.Module_ModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WikiPages", "Module_ModuleId", "dbo.Modules");
            DropIndex("dbo.WikiPages", new[] { "Module_ModuleId" });
            DropTable("dbo.WikiPages");
        }
    }
}
