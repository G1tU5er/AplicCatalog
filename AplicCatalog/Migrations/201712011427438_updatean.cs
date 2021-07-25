namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatean : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MaterieCatalogs", newName: "CatalogMateries");
            DropForeignKey("dbo.PlanInvatamants", "Clasa_IdClasa", "dbo.Clasas");
            DropForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.PlanInvatamants", new[] { "Materie_IdMaterie" });
            DropPrimaryKey("dbo.CatalogMateries");
            CreateTable(
                "dbo.ClasaPlanInvatamants",
                c => new
                    {
                        Clasa_IdClasa = c.Int(nullable: false),
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clasa_IdClasa, t.PlanInvatamant_IdPlan })
                .ForeignKey("dbo.Clasas", t => t.Clasa_IdClasa, cascadeDelete: true)
                .ForeignKey("dbo.PlanInvatamants", t => t.PlanInvatamant_IdPlan, cascadeDelete: true)
                .Index(t => t.Clasa_IdClasa)
                .Index(t => t.PlanInvatamant_IdPlan);
            
            CreateTable(
                "dbo.MateriePlanInvatamants",
                c => new
                    {
                        Materie_IdMaterie = c.Int(nullable: false),
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Materie_IdMaterie, t.PlanInvatamant_IdPlan })
                .ForeignKey("dbo.Materies", t => t.Materie_IdMaterie, cascadeDelete: true)
                .ForeignKey("dbo.PlanInvatamants", t => t.PlanInvatamant_IdPlan, cascadeDelete: true)
                .Index(t => t.Materie_IdMaterie)
                .Index(t => t.PlanInvatamant_IdPlan);
            
            AddColumn("dbo.PlanInvatamants", "AnScolar_IdAn", c => c.Int());
            AddPrimaryKey("dbo.CatalogMateries", new[] { "Catalog_IdNota", "Materie_IdMaterie" });
            CreateIndex("dbo.PlanInvatamants", "AnScolar_IdAn");
            AddForeignKey("dbo.PlanInvatamants", "AnScolar_IdAn", "dbo.AnScolars", "IdAn");
            DropColumn("dbo.PlanInvatamants", "Clasa_IdClasa");
            DropColumn("dbo.PlanInvatamants", "Materie_IdMaterie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanInvatamants", "Materie_IdMaterie", c => c.Int());
            AddColumn("dbo.PlanInvatamants", "Clasa_IdClasa", c => c.Int());
            DropForeignKey("dbo.PlanInvatamants", "AnScolar_IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.ClasaPlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.ClasaPlanInvatamants", "Clasa_IdClasa", "dbo.Clasas");
            DropIndex("dbo.MateriePlanInvatamants", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.MateriePlanInvatamants", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.ClasaPlanInvatamants", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.ClasaPlanInvatamants", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.PlanInvatamants", new[] { "AnScolar_IdAn" });
            DropPrimaryKey("dbo.CatalogMateries");
            DropColumn("dbo.PlanInvatamants", "AnScolar_IdAn");
            DropTable("dbo.MateriePlanInvatamants");
            DropTable("dbo.ClasaPlanInvatamants");
            AddPrimaryKey("dbo.CatalogMateries", new[] { "Materie_IdMaterie", "Catalog_IdNota" });
            CreateIndex("dbo.PlanInvatamants", "Materie_IdMaterie");
            CreateIndex("dbo.PlanInvatamants", "Clasa_IdClasa");
            AddForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "Clasa_IdClasa", "dbo.Clasas", "IdClasa");
            RenameTable(name: "dbo.CatalogMateries", newName: "MaterieCatalogs");
        }
    }
}
