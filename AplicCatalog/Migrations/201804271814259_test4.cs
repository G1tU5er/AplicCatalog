namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Incadrares");
            AddColumn("dbo.Incadrares", "IdxIncadrare", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Incadrares", "IdxIncadrare");
            DropColumn("dbo.Incadrares", "IdIncadrare");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Incadrares", "IdIncadrare", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Incadrares");
            DropColumn("dbo.Incadrares", "IdxIncadrare");
            AddPrimaryKey("dbo.Incadrares", "IdIncadrare");
        }
    }
}
