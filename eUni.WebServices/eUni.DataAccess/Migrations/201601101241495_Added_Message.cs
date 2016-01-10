namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Message : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        DateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        From_DomainUserId = c.Int(),
                        To_DomainUserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DomainUsers", t => t.From_DomainUserId)
                .ForeignKey("dbo.DomainUsers", t => t.To_DomainUserId)
                .Index(t => t.From_DomainUserId)
                .Index(t => t.To_DomainUserId);
            
            AddColumn("dbo.Files", "FileName", c => c.String());
            DropColumn("dbo.Files", "Description");
            DropColumn("dbo.Files", "Size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Files", "Description", c => c.String());
            DropForeignKey("dbo.Messages", "To_DomainUserId", "dbo.DomainUsers");
            DropForeignKey("dbo.Messages", "From_DomainUserId", "dbo.DomainUsers");
            DropIndex("dbo.Messages", new[] { "To_DomainUserId" });
            DropIndex("dbo.Messages", new[] { "From_DomainUserId" });
            DropColumn("dbo.Files", "FileName");
            DropTable("dbo.Messages");
        }
    }
}
