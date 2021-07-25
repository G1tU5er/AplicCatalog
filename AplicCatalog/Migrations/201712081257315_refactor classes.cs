namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactorclasses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamantAnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.PlanInvatamantAnScolars", "AnScolar_IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.ClasaPlanInvatamants", "Clasa_IdClasa", "dbo.Clasas");
            DropForeignKey("dbo.ClasaPlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.AnScolars", "Elev_IdElev", "dbo.Elevs");
            DropForeignKey("dbo.Clasas", "Elev_IdElev", "dbo.Elevs");
            DropForeignKey("dbo.CatalogMateries", "Catalog_IdNota", "dbo.Catalogs");
            DropForeignKey("dbo.CatalogMateries", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.ProfesorMateries", "Profesor_IdProfesor", "dbo.Profesors");
            DropForeignKey("dbo.ProfesorMateries", "Materie_IdMaterie", "dbo.Materies");
            DropIndex("dbo.AnScolars", new[] { "Elev_IdElev" });
            DropIndex("dbo.Clasas", new[] { "Elev_IdElev" });
            DropIndex("dbo.PlanInvatamantAnScolars", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.PlanInvatamantAnScolars", new[] { "AnScolar_IdAn" });
            DropIndex("dbo.ClasaPlanInvatamants", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.ClasaPlanInvatamants", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.CatalogMateries", new[] { "Catalog_IdNota" });
            DropIndex("dbo.CatalogMateries", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.MateriePlanInvatamants", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.MateriePlanInvatamants", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.ProfesorMateries", new[] { "Profesor_IdProfesor" });
            DropIndex("dbo.ProfesorMateries", new[] { "Materie_IdMaterie" });
            AddColumn("dbo.AnScolars", "InceputAnScolar", c => c.Int(nullable: false));
            AddColumn("dbo.AnScolars", "SfarsitAnScolar", c => c.Int(nullable: false));
            AddColumn("dbo.PlanInvatamants", "Nume", c => c.String(nullable: false));
            AddColumn("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", c => c.Int());
            AddColumn("dbo.Clasas", "Descriere", c => c.String());
            AddColumn("dbo.Clasas", "PlanDeInvatamant_IdPlan", c => c.Int());
            AddColumn("dbo.Materies", "Comentariu", c => c.String());
            AddColumn("dbo.Materies", "Catalog_IdNota", c => c.Int());
            AddColumn("dbo.Materies", "PlanInvatamant_IdPlan", c => c.Int());
            AddColumn("dbo.Materies", "Profesor_IdProfesor", c => c.Int());
            CreateIndex("dbo.Materies", "Catalog_IdNota");
            CreateIndex("dbo.Materies", "PlanInvatamant_IdPlan");
            CreateIndex("dbo.Materies", "Profesor_IdProfesor");
            CreateIndex("dbo.Clasas", "PlanDeInvatamant_IdPlan");
            CreateIndex("dbo.PlanInvatamants", "AnDeValabilitate_IdAn");
            AddForeignKey("dbo.Materies", "Catalog_IdNota", "dbo.Catalogs", "IdNota");
            AddForeignKey("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", "dbo.AnScolars", "IdAn");
            AddForeignKey("dbo.Materies", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
            AddForeignKey("dbo.Clasas", "PlanDeInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
            AddForeignKey("dbo.Materies", "Profesor_IdProfesor", "dbo.Profesors", "IdProfesor");
            DropColumn("dbo.AnScolars", "An");
            DropColumn("dbo.AnScolars", "Elev_IdElev");
            DropColumn("dbo.Clasas", "NrMaxAbsente");
            DropColumn("dbo.Clasas", "Elev_IdElev");
            DropTable("dbo.PlanInvatamantAnScolars");
            DropTable("dbo.ClasaPlanInvatamants");
            DropTable("dbo.CatalogMateries");
            DropTable("dbo.MateriePlanInvatamants");
            DropTable("dbo.ProfesorMateries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProfesorMateries",
                c => new
                    {
                        Profesor_IdProfesor = c.Int(nullable: false),
                        Materie_IdMaterie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profesor_IdProfesor, t.Materie_IdMaterie });
            
            CreateTable(
                "dbo.MateriePlanInvatamants",
                c => new
                    {
                        Materie_IdMaterie = c.Int(nullable: false),
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Materie_IdMaterie, t.PlanInvatamant_IdPlan });
            
            CreateTable(
                "dbo.CatalogMateries",
                c => new
                    {
                        Catalog_IdNota = c.Int(nullable: false),
                        Materie_IdMaterie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Catalog_IdNota, t.Materie_IdMaterie });
            
            CreateTable(
                "dbo.ClasaPlanInvatamants",
                c => new
                    {
                        Clasa_IdClasa = c.Int(nullable: false),
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clasa_IdClasa, t.PlanInvatamant_IdPlan });
            
            CreateTable(
                "dbo.PlanInvatamantAnScolars",
                c => new
                    {
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                        AnScolar_IdAn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanInvatamant_IdPlan, t.AnScolar_IdAn });
            
            AddColumn("dbo.Clasas", "Elev_IdElev", c => c.Int());
            AddColumn("dbo.Clasas", "NrMaxAbsente", c => c.Int(nullable: false));
            AddColumn("dbo.AnScolars", "Elev_IdElev", c => c.Int());
            AddColumn("dbo.AnScolars", "An", c => c.Int(nullable: false));
            DropForeignKey("dbo.Materies", "Profesor_IdProfesor", "dbo.Profesors");
            DropForeignKey("dbo.Clasas", "PlanDeInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.Materies", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.PlanInvatamants", "AnDeValabilitate_IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.Materies", "Catalog_IdNota", "dbo.Catalogs");
            DropIndex("dbo.PlanInvatamants", new[] { "AnDeValabilitate_IdAn" });
            DropIndex("dbo.Clasas", new[] { "PlanDeInvatamant_IdPlan" });
            DropIndex("dbo.Materies", new[] { "Profesor_IdProfesor" });
            DropIndex("dbo.Materies", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.Materies", new[] { "Catalog_IdNota" });
            DropColumn("dbo.Materies", "Profesor_IdProfesor");
            DropColumn("dbo.Materies", "PlanInvatamant_IdPlan");
            DropColumn("dbo.Materies", "Catalog_IdNota");
            DropColumn("dbo.Materies", "Comentariu");
            DropColumn("dbo.Clasas", "PlanDeInvatamant_IdPlan");
            DropColumn("dbo.Clasas", "Descriere");
            DropColumn("dbo.PlanInvatamants", "AnDeValabilitate_IdAn");
            DropColumn("dbo.PlanInvatamants", "Nume");
            DropColumn("dbo.AnScolars", "SfarsitAnScolar");
            DropColumn("dbo.AnScolars", "InceputAnScolar");
            CreateIndex("dbo.ProfesorMateries", "Materie_IdMaterie");
            CreateIndex("dbo.ProfesorMateries", "Profesor_IdProfesor");
            CreateIndex("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan");
            CreateIndex("dbo.MateriePlanInvatamants", "Materie_IdMaterie");
            CreateIndex("dbo.CatalogMateries", "Materie_IdMaterie");
            CreateIndex("dbo.CatalogMateries", "Catalog_IdNota");
            CreateIndex("dbo.ClasaPlanInvatamants", "PlanInvatamant_IdPlan");
            CreateIndex("dbo.ClasaPlanInvatamants", "Clasa_IdClasa");
            CreateIndex("dbo.PlanInvatamantAnScolars", "AnScolar_IdAn");
            CreateIndex("dbo.PlanInvatamantAnScolars", "PlanInvatamant_IdPlan");
            CreateIndex("dbo.Clasas", "Elev_IdElev");
            CreateIndex("dbo.AnScolars", "Elev_IdElev");
            AddForeignKey("dbo.ProfesorMateries", "Materie_IdMaterie", "dbo.Materies", "IdMaterie", cascadeDelete: true);
            AddForeignKey("dbo.ProfesorMateries", "Profesor_IdProfesor", "dbo.Profesors", "IdProfesor", cascadeDelete: true);
            AddForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan", cascadeDelete: true);
            AddForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie", cascadeDelete: true);
            AddForeignKey("dbo.CatalogMateries", "Materie_IdMaterie", "dbo.Materies", "IdMaterie", cascadeDelete: true);
            AddForeignKey("dbo.CatalogMateries", "Catalog_IdNota", "dbo.Catalogs", "IdNota", cascadeDelete: true);
            AddForeignKey("dbo.Clasas", "Elev_IdElev", "dbo.Elevs", "IdElev");
            AddForeignKey("dbo.AnScolars", "Elev_IdElev", "dbo.Elevs", "IdElev");
            AddForeignKey("dbo.ClasaPlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan", cascadeDelete: true);
            AddForeignKey("dbo.ClasaPlanInvatamants", "Clasa_IdClasa", "dbo.Clasas", "IdClasa", cascadeDelete: true);
            AddForeignKey("dbo.PlanInvatamantAnScolars", "AnScolar_IdAn", "dbo.AnScolars", "IdAn", cascadeDelete: true);
            AddForeignKey("dbo.PlanInvatamantAnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan", cascadeDelete: true);
        }
    }
}
