namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifcatalog : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Elevs", "Catalog_IdNota", "dbo.Catalogs");
            DropForeignKey("dbo.Materies", "Catalog_IdNota", "dbo.Catalogs");
            DropIndex("dbo.Materies", new[] { "Catalog_IdNota" });
            DropIndex("dbo.Elevs", new[] { "Catalog_IdNota" });
            AddColumn("dbo.Catalogs", "IdMaterie", c => c.Int(nullable: false));
            AddColumn("dbo.Catalogs", "IdxIncadrare", c => c.Int(nullable: false));
            CreateIndex("dbo.Catalogs", "IdMaterie");
            CreateIndex("dbo.Catalogs", "IdxIncadrare");
            AddForeignKey("dbo.Catalogs", "IdxIncadrare", "dbo.Incadrares", "IdxIncadrare");
            AddForeignKey("dbo.Catalogs", "IdMaterie", "dbo.Materies", "IdMaterie");
            DropColumn("dbo.Materies", "Catalog_IdNota");
            DropColumn("dbo.Elevs", "Catalog_IdNota");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Elevs", "Catalog_IdNota", c => c.Int());
            AddColumn("dbo.Materies", "Catalog_IdNota", c => c.Int());
            DropForeignKey("dbo.Catalogs", "IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.Catalogs", "IdxIncadrare", "dbo.Incadrares");
            DropIndex("dbo.Catalogs", new[] { "IdxIncadrare" });
            DropIndex("dbo.Catalogs", new[] { "IdMaterie" });
            DropColumn("dbo.Catalogs", "IdxIncadrare");
            DropColumn("dbo.Catalogs", "IdMaterie");
            CreateIndex("dbo.Elevs", "Catalog_IdNota");
            CreateIndex("dbo.Materies", "Catalog_IdNota");
            AddForeignKey("dbo.Materies", "Catalog_IdNota", "dbo.Catalogs", "IdNota");
            AddForeignKey("dbo.Elevs", "Catalog_IdNota", "dbo.Catalogs", "IdNota");
        }
    }
}
