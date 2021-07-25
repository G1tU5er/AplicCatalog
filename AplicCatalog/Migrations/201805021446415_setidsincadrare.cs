namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setidsincadrare : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Incadrares", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.Incadrares", new[] { "Elev_IdElev" });
            RenameColumn(table: "dbo.Incadrares", name: "Clasa_IdClasa", newName: "idClasa");
            RenameColumn(table: "dbo.Incadrares", name: "Elev_IdElev", newName: "IdElev");
            AddColumn("dbo.Incadrares", "idAnScolar", c => c.Int(nullable: false));
            AlterColumn("dbo.Incadrares", "idClasa", c => c.Int(nullable: false));
            AlterColumn("dbo.Incadrares", "IdElev", c => c.Int(nullable: false));
            CreateIndex("dbo.Incadrares", "IdElev");
            CreateIndex("dbo.Incadrares", "idClasa");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Incadrares", new[] { "idClasa" });
            DropIndex("dbo.Incadrares", new[] { "IdElev" });
            AlterColumn("dbo.Incadrares", "IdElev", c => c.Int());
            AlterColumn("dbo.Incadrares", "idClasa", c => c.Int());
            DropColumn("dbo.Incadrares", "idAnScolar");
            RenameColumn(table: "dbo.Incadrares", name: "IdElev", newName: "Elev_IdElev");
            RenameColumn(table: "dbo.Incadrares", name: "idClasa", newName: "Clasa_IdClasa");
            CreateIndex("dbo.Incadrares", "Elev_IdElev");
            CreateIndex("dbo.Incadrares", "Clasa_IdClasa");
        }
    }
}
