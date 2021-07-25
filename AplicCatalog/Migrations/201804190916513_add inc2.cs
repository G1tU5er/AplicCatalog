namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinc2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Incadrares", "IdIncadrare", "dbo.Elevs");
            DropIndex("dbo.Incadrares", new[] { "IdIncadrare" });
            DropPrimaryKey("dbo.Incadrares");
            AddColumn("dbo.Incadrares", "Elev_IdElev", c => c.Int());
            AlterColumn("dbo.Incadrares", "IdIncadrare", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Incadrares", "IdIncadrare");
            CreateIndex("dbo.Incadrares", "Elev_IdElev");
            AddForeignKey("dbo.Incadrares", "Elev_IdElev", "dbo.Elevs", "IdElev");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Incadrares", "Elev_IdElev", "dbo.Elevs");
            DropIndex("dbo.Incadrares", new[] { "Elev_IdElev" });
            DropPrimaryKey("dbo.Incadrares");
            AlterColumn("dbo.Incadrares", "IdIncadrare", c => c.Int(nullable: false));
            DropColumn("dbo.Incadrares", "Elev_IdElev");
            AddPrimaryKey("dbo.Incadrares", "IdIncadrare");
            CreateIndex("dbo.Incadrares", "IdIncadrare");
            AddForeignKey("dbo.Incadrares", "IdIncadrare", "dbo.Elevs", "IdElev");
        }
    }
}
