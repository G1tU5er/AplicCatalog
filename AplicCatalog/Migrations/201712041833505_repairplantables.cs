namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repairplantables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "an_IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.PlanInvatamants", "clasa_IdClasa", "dbo.Clasas");
            DropForeignKey("dbo.PlanInvatamants", "materie_IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "an_IdAn" });
            DropIndex("dbo.PlanInvatamants", new[] { "clasa_IdClasa" });
            DropIndex("dbo.PlanInvatamants", new[] { "materie_IdMaterie" });
            CreateTable(
                "dbo.PlanInvatamantAnScolars",
                c => new
                    {
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                        AnScolar_IdAn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanInvatamant_IdPlan, t.AnScolar_IdAn })
                .ForeignKey("dbo.PlanInvatamants", t => t.PlanInvatamant_IdPlan, cascadeDelete: true)
                .ForeignKey("dbo.AnScolars", t => t.AnScolar_IdAn, cascadeDelete: true)
                .Index(t => t.PlanInvatamant_IdPlan)
                .Index(t => t.AnScolar_IdAn);
            
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
            
            DropColumn("dbo.PlanInvatamants", "an_IdAn");
            DropColumn("dbo.PlanInvatamants", "clasa_IdClasa");
            DropColumn("dbo.PlanInvatamants", "materie_IdMaterie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanInvatamants", "materie_IdMaterie", c => c.Int());
            AddColumn("dbo.PlanInvatamants", "clasa_IdClasa", c => c.Int());
            AddColumn("dbo.PlanInvatamants", "an_IdAn", c => c.Int());
            DropForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.ClasaPlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.ClasaPlanInvatamants", "Clasa_IdClasa", "dbo.Clasas");
            DropForeignKey("dbo.PlanInvatamantAnScolars", "AnScolar_IdAn", "dbo.AnScolars");
            DropForeignKey("dbo.PlanInvatamantAnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.MateriePlanInvatamants", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.MateriePlanInvatamants", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.ClasaPlanInvatamants", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.ClasaPlanInvatamants", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.PlanInvatamantAnScolars", new[] { "AnScolar_IdAn" });
            DropIndex("dbo.PlanInvatamantAnScolars", new[] { "PlanInvatamant_IdPlan" });
            DropTable("dbo.MateriePlanInvatamants");
            DropTable("dbo.ClasaPlanInvatamants");
            DropTable("dbo.PlanInvatamantAnScolars");
            CreateIndex("dbo.PlanInvatamants", "materie_IdMaterie");
            CreateIndex("dbo.PlanInvatamants", "clasa_IdClasa");
            CreateIndex("dbo.PlanInvatamants", "an_IdAn");
            AddForeignKey("dbo.PlanInvatamants", "materie_IdMaterie", "dbo.Materies", "IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "clasa_IdClasa", "dbo.Clasas", "IdClasa");
            AddForeignKey("dbo.PlanInvatamants", "an_IdAn", "dbo.AnScolars", "IdAn");
        }
    }
}
