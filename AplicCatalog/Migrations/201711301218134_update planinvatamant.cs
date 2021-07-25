namespace AplicCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateplaninvatamant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanInvatamants", "Clasa_IdClasa", "dbo.Clasas");
            DropForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropIndex("dbo.PlanInvatamants", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.PlanInvatamants", new[] { "Materie_IdMaterie" });
            CreateTable(
                "dbo.PlanInvatamantClasas",
                c => new
                    {
                        PlanInvatamant_IdPlan = c.Int(nullable: false),
                        Clasa_IdClasa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanInvatamant_IdPlan, t.Clasa_IdClasa })
                .ForeignKey("dbo.PlanInvatamants", t => t.PlanInvatamant_IdPlan, cascadeDelete: true)
                .ForeignKey("dbo.Clasas", t => t.Clasa_IdClasa, cascadeDelete: true)
                .Index(t => t.PlanInvatamant_IdPlan)
                .Index(t => t.Clasa_IdClasa);
            
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
            
            AddColumn("dbo.AnScolars", "PlanInvatamant_IdPlan", c => c.Int());
            CreateIndex("dbo.AnScolars", "PlanInvatamant_IdPlan");
            AddForeignKey("dbo.AnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants", "IdPlan");
            DropColumn("dbo.PlanInvatamants", "Clasa_IdClasa");
            DropColumn("dbo.PlanInvatamants", "Materie_IdMaterie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanInvatamants", "Materie_IdMaterie", c => c.Int());
            AddColumn("dbo.PlanInvatamants", "Clasa_IdClasa", c => c.Int());
            DropForeignKey("dbo.MateriePlanInvatamants", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.MateriePlanInvatamants", "Materie_IdMaterie", "dbo.Materies");
            DropForeignKey("dbo.PlanInvatamantClasas", "Clasa_IdClasa", "dbo.Clasas");
            DropForeignKey("dbo.PlanInvatamantClasas", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropForeignKey("dbo.AnScolars", "PlanInvatamant_IdPlan", "dbo.PlanInvatamants");
            DropIndex("dbo.MateriePlanInvatamants", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.MateriePlanInvatamants", new[] { "Materie_IdMaterie" });
            DropIndex("dbo.PlanInvatamantClasas", new[] { "Clasa_IdClasa" });
            DropIndex("dbo.PlanInvatamantClasas", new[] { "PlanInvatamant_IdPlan" });
            DropIndex("dbo.AnScolars", new[] { "PlanInvatamant_IdPlan" });
            DropColumn("dbo.AnScolars", "PlanInvatamant_IdPlan");
            DropTable("dbo.MateriePlanInvatamants");
            DropTable("dbo.PlanInvatamantClasas");
            CreateIndex("dbo.PlanInvatamants", "Materie_IdMaterie");
            CreateIndex("dbo.PlanInvatamants", "Clasa_IdClasa");
            AddForeignKey("dbo.PlanInvatamants", "Materie_IdMaterie", "dbo.Materies", "IdMaterie");
            AddForeignKey("dbo.PlanInvatamants", "Clasa_IdClasa", "dbo.Clasas", "IdClasa");
        }
    }
}
